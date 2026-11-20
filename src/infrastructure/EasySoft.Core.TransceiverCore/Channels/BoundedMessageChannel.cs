namespace EasySoft.Core.TransceiverCore.Channels;

/// <summary>
/// 有界缓冲通道工具类，基于 System.Threading.Channels 封装
/// 用于异常收集模块的流量削峰，区分主入口通道 / Sink子缓冲通道
/// 禁止 Wait 阻塞模式，全部采用非阻塞入队，保护Web业务链路
/// </summary>
/// <typeparam name="T">消息实体类型，推荐只读引用对象，分发仅传递引用</typeparam>
public sealed class BoundedMessageChannel<T>
{
    private readonly Channel<T> _channel;

    // 原子统计指标
    private long _enqueueSuccess;
    private long _enqueueDropped;
    private long _readTotal;

    /// <summary>
    /// 成功入队总数
    /// </summary>
    public long EnqueueSuccess => Volatile.Read(ref _enqueueSuccess);

    /// <summary>
    /// 队列满触发淘汰/丢弃总数
    /// </summary>
    public long EnqueueDropped => Volatile.Read(ref _enqueueDropped);

    /// <summary>
    /// 成功读取消费总数
    /// </summary>
    public long ReadTotal => Volatile.Read(ref _readTotal);

    /// <summary>
    /// 通道最大容量
    /// </summary>
    public int Capacity { get; }

    /// <summary>
    /// 通道满时策略
    /// </summary>
    public BoundedChannelFullMode FullMode { get; }

    /// <summary>
    /// 近似队列存量，仅监控参考，非原子精确快照
    /// </summary>
    public int ApproximateCount => _channel.Reader.Count;

    /// <summary>
    /// 私有构造，由静态工厂创建，约束策略
    /// </summary>
    /// <param name="capacity">最大容量</param>
    /// <param name="fullMode">满溢策略</param>
    private BoundedMessageChannel(int capacity, BoundedChannelFullMode fullMode)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(capacity),
                "通道容量必须大于0"
            );
        }

        // Web场景严禁阻塞入队，直接禁用Wait模式
        if (fullMode == BoundedChannelFullMode.Wait)
        {
            throw new NotSupportedException(
                "Wait阻塞模式不允许用于业务缓冲通道，会卡住请求线程"
            );
        }

        Capacity = capacity;
        FullMode = fullMode;

        var opt = new BoundedChannelOptions(capacity)
        {
            FullMode = fullMode,
            SingleReader = false,
            SingleWriter = false,
            AllowSynchronousContinuations = false
        };

        _channel = Channel.CreateBounded<T>(opt);
    }

    /// <summary>
    /// 创建【主入口通道】
    /// 策略：DropOldest，队列满时淘汰队首旧消息，优先保留最新异常样本
    /// 默认容量：2000；适合异常收集第一层总缓冲，前置削峰
    /// </summary>
    public static BoundedMessageChannel<T> CreateMain(int capacity = 2000)
    {
        return new BoundedMessageChannel<T>(capacity, BoundedChannelFullMode.DropOldest);
    }

    /// <summary>
    /// 创建【Sink子缓冲通道】（FileSink / MqSink 共用）
    /// 策略：DropWrite，队列满直接丢弃新来条目，不淘汰存量
    /// 默认容量：64；作用仅平滑IO/网络短时抖动，不作为风暴缓冲
    /// </summary>
    public static BoundedMessageChannel<T> CreateSinkBuffer(int capacity = 64)
    {
        return new BoundedMessageChannel<T>(capacity, BoundedChannelFullMode.DropWrite);
    }

    /// <summary>
    /// 非阻塞尝试入队，Web捕获线程调用，绝不阻塞接口
    /// </summary>
    /// <param name="item">待投递消息</param>
    /// <returns>true=入队成功；false=通道已满，触发淘汰/丢弃</returns>
    public bool TryEnqueue(T item)
    {
        if (_channel.Writer.TryWrite(item))
        {
            Interlocked.Increment(ref _enqueueSuccess);
            return true;
        }

        Interlocked.Increment(ref _enqueueDropped);
        return false;
    }

    /// <summary>
    /// 异步读取一条消息，给消费循环使用
    /// 优先读取队列内已有数据；仅当队列为空时才等待新消息
    /// 通道Complete并且无剩余消息时返回default(T)
    /// 取消令牌仅在队列为空等待阶段生效；队列有数据时优先返回数据，不会因令牌取消抛异常
    /// </summary>
    /// <param name="ct">取消令牌</param>
    public async ValueTask<T> ReadAsync(CancellationToken ct = default)
    {
        // 优先尝试读取已有数据，不等待
        if (_channel.Reader.TryRead(out var item))
        {
            Interlocked.Increment(ref _readTotal);
            return item;
        }

        // 队列空，进入等待阶段，这里才会响应取消令牌
        try
        {
            while (await _channel.Reader.WaitToReadAsync(ct))
            {
                if (_channel.Reader.TryRead(out item))
                {
                    Interlocked.Increment(ref _readTotal);
                    return item;
                }
            }
        }
        catch (OperationCanceledException)
        {
            // 等待期间收到取消，再次尝试读一次队列残留数据
            if (_channel.Reader.TryRead(out item))
            {
                Interlocked.Increment(ref _readTotal);
                return item;
            }

            // 队列无残留，返回default标记结束
            return default;
        }

        // 通道已完成、无更多数据
        return default;
    }

    /// <summary>
    /// 非阻塞尝试读取
    /// </summary>
    public bool TryRead(out T item)
    {
        if (_channel.Reader.TryRead(out item))
        {
            Interlocked.Increment(ref _readTotal);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 标记写入完成，不再接受新消息；消费端读完现有消息后正常退出
    /// 应用优雅关闭时调用
    /// </summary>
    public void Complete()
    {
        _channel.Writer.Complete();
    }

    /// <summary>
    /// 原始Reader暴露，供高级场景直接使用
    /// </summary>
    public ChannelReader<T> Reader => _channel.Reader;
}