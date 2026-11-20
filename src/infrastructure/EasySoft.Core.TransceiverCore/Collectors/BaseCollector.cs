using EasySoft.Core.TransceiverCore.Channels;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.TransceiverCore.Collectors;

/// <summary>
/// 异常收集器｜主谐振通道 + File/Transfer 双二级通道
/// </summary>
/// <remarks>
/// <para>设计目标：提供可控弹性缓冲平衡点，拒绝无边界无限缓冲堆积，把内存上限控制在可预测范围。</para>
/// <para>遵循如下策略：</para>
/// <para>主通道：全局排队硬配额，第一级削峰熔断，决定整个收集器的内存预算上限；通道满时采用 DropOldest 策略，直接丢弃最早入队消息。</para>
/// <para>二级子通道：弹性缓冲层，推荐倍率区间 [2.0, 2.5]。用于消化短时间连续瞬时脉冲，同时隔离下游 IO/网络阻塞，避免阻塞反向传导到上游入队侧。</para>
/// <para>底层磁盘、网络延迟受系统、链路、远端服务影响很大；缓冲仅用于平滑短时抖动，不能掩盖持续性下游故障；子类实现的写入逻辑务必自带独立超时控制。</para>
/// <para>每个子通道支持独立开关控制：关闭后不再接收转发过来的新消息，通道内已经排队的存量消息会继续消费直至排空，不会直接清空队列。空闲空轮循环使用自适应指数退避，降低长期空队列场景下的CPU周转开销。</para>
/// <para>保留独立容量重载作为高级逃逸口，日常业务优先使用主容量+倍率构造方式。</para>
/// <para>本实现仅为内存缓冲，不具备持久化落地保障；组件关闭、释放时通道内尚未消费完成的滞留消息会直接丢弃，丢弃数量计入 SubTermDrop，该行为属于框架既定设计契约。</para>
/// <para>数据流：业务实体入队 → 在Enqueue阶段一次性序列化为JSON字符串封装为包写入主通道；转发循环读取主通道，广播分发至所有处于启用状态的File/Transfer二级通道；基类统一驱动转发、批量消费、指标统计与生命周期管理，子类仅负责实现底层文件写入、Transfer发送的业务抽象方法。</para>
/// </remarks>
/// <typeparam name="T">原始消息实体类型</typeparam>
public abstract class BaseCollector<T> : IDisposable
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// 主通道，接收上游Enqueue送入的消息包
    /// </summary>
    private readonly Channel<MessagePacket> _mainChannel;

    /// <summary>
    /// 文件输出子通道实例
    /// </summary>
    protected SinkSubChannel FileSink { get; private set; }

    /// <summary>
    /// Transfer输出子通道实例
    /// </summary>
    private SinkSubChannel TransferSink { get; set; }

    /// <summary>
    /// 全局取消源，统一控制转发、所有消费任务的停止信号
    /// </summary>
    private readonly CancellationTokenSource _cancellationTokenSource;

    /// <summary>
    /// 主转发循环任务句柄
    /// </summary>
    private Task _forwardTask;

    /// <summary>
    /// 是否已调用Start启动后台循环
    /// </summary>
    private bool _whetherStarted;

    /// <summary>
    /// 启动流程锁，保护Enqueue入口与Close流程并发安全
    /// </summary>
    private readonly Lock _startLock = new();

    /// <summary>
    /// 组件是否已经进入关闭状态
    /// </summary>
    private bool _whetherClosed;

    /// <summary>
    /// 关闭流程锁，保护Enqueue入口与Close流程并发安全
    /// </summary>
    private readonly Lock _closeLock = new();

    /// <summary>
    /// 主通道配置容量上限
    /// </summary>
    public int MainBufferCapacity { get; }

    /// <summary>
    /// File二级通道配置容量上限
    /// </summary>
    public int FileSinkCapacity { get; }

    /// <summary>
    /// Transfer二级通道配置容量上限
    /// </summary>
    public int TransferSinkCapacity { get; }

    /// <summary>
    /// 默认简洁构造：主容量 + 弹性倍率，双Sink使用相同缓冲容量，通道默认全部开启
    /// </summary>
    /// <param name="loggerFactory">日志记录器工厂</param>
    /// <param name="mainBufferCapacity">主通道全局配额上限</param>
    /// <param name="sinkCapacityMultiplier">二级缓冲弹性倍率，推荐 2.0 ~ 2.5</param>
    protected BaseCollector(
        ILoggerFactory loggerFactory,
        int mainBufferCapacity,
        double sinkCapacityMultiplier
    )
        : this(
            loggerFactory,
            mainBufferCapacity,
            CalcCapacity(mainBufferCapacity, sinkCapacityMultiplier),
            CalcCapacity(mainBufferCapacity, sinkCapacityMultiplier)
        )
    {
    }

    /// <summary>
    /// 高级逃逸重载：独立分别指定 File、Transfer 二级通道缓冲容量，通道默认全部开启
    /// </summary>
    /// <param name="loggerFactory">日志记录器工厂</param>
    /// <param name="mainBufferCapacity">主通道全局配额上限</param>
    /// <param name="fileSinkCapacity">File子通道独立容量上限</param>
    /// <param name="transferSinkCapacity">Transfer子通道独立容量上限</param>
    protected BaseCollector(
        ILoggerFactory loggerFactory,
        int mainBufferCapacity,
        int fileSinkCapacity,
        int transferSinkCapacity
    )
    {
        Logger = loggerFactory.CreateLogger<T>();

        if (mainBufferCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(mainBufferCapacity));
        }

        if (fileSinkCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(fileSinkCapacity));
        }

        if (transferSinkCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(transferSinkCapacity));
        }

        MainBufferCapacity = mainBufferCapacity;
        FileSinkCapacity = fileSinkCapacity;
        TransferSinkCapacity = transferSinkCapacity;

        var mainOpts = new BoundedChannelOptions(MainBufferCapacity)
        {
            FullMode = BoundedChannelFullMode.DropOldest
        };
        _mainChannel = Channel.CreateBounded<MessagePacket>(mainOpts);

        _cancellationTokenSource = new CancellationTokenSource();
        _whetherClosed = false;

        FileSink = BuildSinkChannel(FileSinkCapacity);
        TransferSink = BuildSinkChannel(TransferSinkCapacity);

        // 默认两个输出通道均开启
        FileSink.Enabled = true;
        TransferSink.Enabled = true;
    }

    /// <summary>
    /// 根据主通道容量和倍率，计算二级通道容量，保底最小1
    /// </summary>
    private static int CalcCapacity(int main, double multiplier)
    {
        var val = (int)Math.Round(main * multiplier);

        return val < 1 ? 1 : val;
    }

    /// <summary>
    /// 构造单个Sink子通道、绑定消费任务
    /// </summary>
    private SinkSubChannel BuildSinkChannel(int capacity)
    {
        var opts = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.DropOldest
        };

        var channel = Channel.CreateBounded<MessagePacket>(opts);

        var sink = new SinkSubChannel
        {
            Channel = channel
        };

        return sink;
    }

    /// <summary>
    /// 主动启动转发与所有Sink消费循环，仅允许调用一次
    /// <para>建议在DI实例构造完成、全部依赖（Logger、下游客户端等）就绪后调用</para>
    /// </summary>
    /// <exception cref="InvalidOperationException">重复启动或已经Close时抛出</exception>
    public void Start()
    {
        lock (_startLock)
        {
            if (_whetherClosed)
            {
                throw new InvalidOperationException("收集器已关闭，无法启动。");
            }

            if (_whetherStarted)
            {
                throw new InvalidOperationException("收集器已经启动，不可重复调用Start。");
            }

            _forwardTask = RunForwardLoop(_cancellationTokenSource.Token);
            FileSink.ConsumerTask = RunSinkConsumerLoop(FileSink, _cancellationTokenSource.Token);
            TransferSink.ConsumerTask = RunSinkConsumerLoop(TransferSink, _cancellationTokenSource.Token);

            _whetherStarted = true;
        }
    }

    /// <summary>
    /// 业务入队入口
    /// </summary>
    /// <remarks>
    ///组件关闭状态下直接丢弃；未关闭时序列化实体，写入主通道，使用TryWrite，主通道满自动走DropOldest策略
    /// </remarks>
    /// <param name="item">待投递原始实体</param>
    public bool Publish(T item)
    {
        lock (_closeLock)
        {
            if (_whetherClosed)
            {
                return false;
            }

            var json = Serialize(item);

            return _mainChannel.Writer.TryWrite(new MessagePacket(json));
        }
    }

    /// <summary>
    /// 序列化抽象：实体 → JSON字符串，仅在Enqueue入队阶段执行一次
    /// </summary>
    protected virtual string Serialize(T item)
    {
        return JsonConvertAssist.SerializeObject(item);
    }

    /// <summary>
    /// 按需反序列化抽象：JSON文本还原实体，由子类业务逻辑按需自主调用，框架不强制使用
    /// </summary>
    protected virtual T Deserialize(string json)
    {
        return JsonConvertAssist.DeserializeObject<T>(json);
    }

    /// <summary>
    /// 文件写入抽象方法，传入预序列化完成的JSON文本，子类实现落地逻辑
    /// </summary>
    protected abstract Task WriteFileAsync(string json, CancellationToken ct);

    /// <summary>
    /// Transfer发送抽象方法，传入预序列化完成的JSON文本，子类实现外部发送逻辑
    /// </summary>
    protected abstract Task TransferAsync(string json, CancellationToken ct);

    /// <summary>
    /// 获取当前全量指标快照
    /// <para>计数器为Interlocked原子累加，线程安全；Channel.Reader.Count仅为近似水位，不适合精确对账</para>
    /// </summary>
    /// <returns>收集器整体指标快照</returns>
    public CollectorMetrics GetMetrics()
    {
        lock (_closeLock)
        {
            return new CollectorMetrics(
                _mainChannel.Reader.Count,
                FileSink.Snapshot(),
                TransferSink.Snapshot()
            );
        }
    }

    /// <summary>
    /// 启用File输出通道，转发循环恢复向该通道投递新消息
    /// </summary>
    public void EnableFile()
    {
        FileSink.Enabled = true;
    }

    /// <summary>
    /// 禁用File输出通道；转发侧停止投递新消息，通道存量排队消息继续消费直到排空
    /// </summary>
    public void DisableFile()
    {
        FileSink.Enabled = false;
    }

    /// <summary>
    /// 启用Transfer输出通道，转发循环恢复向该通道投递新消息
    /// </summary>
    public void EnableTransfer()
    {
        TransferSink.Enabled = true;
    }

    /// <summary>
    /// 禁用Transfer输出通道；转发侧停止投递新消息，通道存量排队消息继续消费直到排空
    /// </summary>
    public void DisableTransfer()
    {
        TransferSink.Enabled = false;
    }

    /// <summary>
    /// 主转发循环：持续读取主通道消息包，广播分发到所有已启用的二级Sink通道
    /// </summary>
    /// <remarks>
    /// 仅传递包引用，不复制字符串；主通道长期为空时使用独立自适应退避降低空轮CPU消耗
    /// </remarks>
    private async Task RunForwardLoop(CancellationToken ct)
    {
        var backoff = new AdaptiveBackoff();

        while (!ct.IsCancellationRequested)
        {
            try
            {
                if (_mainChannel.Reader.TryRead(out var packet))
                {
                    backoff.Reset();

                    WriteToSink(FileSink, packet);
                    WriteToSink(TransferSink, packet);

                    continue;
                }

                await backoff.Sleep(ct);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Logger.LogAdvanceWarning($"ForwardLoop error:{ex.Message}");

                await Task.Delay(10, ct);

                backoff.Reset();
            }
        }
    }

    /// <summary>
    /// 向单个Sink通道投递消息包，开关关闭直接跳过；通道满触发泄洪计数SubEvict
    /// </summary>
    private void WriteToSink(SinkSubChannel sink, MessagePacket packet)
    {
        if (!sink.Enabled)
        {
            return;
        }

        if (sink.Channel.Writer.TryWrite(packet))
        {
            Interlocked.Increment(ref sink.SubIn);
        }
        else
        {
            Interlocked.Increment(ref sink.SubEvict);
        }
    }

    /// <summary>
    /// 单Sink独立消费循环，基类统一托管
    /// </summary>
    /// <remarks>
    /// 批量读取消息（BatchSize=32），调用子类抽象写入方法，异常兜底、指标统计；通道长期空时自适应退避降空轮开销
    /// </remarks>
    private async Task RunSinkConsumerLoop(SinkSubChannel sink, CancellationToken ct)
    {
        var batch = new List<MessagePacket>();

        const int batchSize = 32;

        var backoff = new AdaptiveBackoff();

        while (!ct.IsCancellationRequested)
        {
            try
            {
                while (batch.Count < batchSize && sink.Channel.Reader.TryRead(out var packet))
                {
                    batch.Add(packet);
                }

                if (batch.Count == 0)
                {
                    await backoff.Sleep(ct);

                    continue;
                }

                backoff.Reset();

                foreach (var pkt in batch)
                {
                    if (ct.IsCancellationRequested)
                    {
                        break;
                    }

                    if (sink == FileSink)
                    {
                         await WriteFileAsync(pkt.JsonText, ct);
                    }
                    else if (sink == TransferSink)
                    {
                        await TransferAsync(pkt.JsonText, ct);
                    }
                }

                Interlocked.Add(ref sink.SubOut, batch.Count);

                batch.Clear();
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Interlocked.Increment(ref sink.PublishFail);

                Logger.LogAdvanceWarning($"SinkConsumerLoop error:{ex.Message}");

                await Task.Delay(10, ct);

                backoff.Reset();
            }
        }
    }

    /// <summary>
    /// 关闭收集器入口与后台任务，停止接收新消息、停止转发与消费；剩余未消费消息丢弃，计入SubTermDrop
    /// <para>幂等：重复调用无副作用</para>
    /// </summary>
    public void Close()
    {
        lock (_closeLock)
        {
            if (_whetherClosed)
            {
                return;
            }

            _whetherClosed = true;

            _mainChannel.Writer.Complete();
            _cancellationTokenSource.Cancel();

            // 清空主通道残留消息
            while (_mainChannel.Reader.TryRead(out _))
            {
            }

            DrainSink(FileSink);
            DrainSink(TransferSink);
        }
    }

    /// <summary>
    /// 标记通道完成并排空，剩余消息计入终止丢弃指标
    /// </summary>
    private void DrainSink(SinkSubChannel sink)
    {
        sink.Channel.Writer.Complete();

        while (sink.Channel.Reader.TryRead(out _))
        {
            Interlocked.Increment(ref sink.SubTermDrop);
        }
    }

    /// <summary>
    /// Dispose流程，先执行Close，再等待后台任务有限时间退出，释放取消源
    /// </summary>
    public void Dispose()
    {
        Close();

        try
        {
            _forwardTask?.Wait(500);
            FileSink.ConsumerTask?.Wait(500);
            TransferSink.ConsumerTask?.Wait(500);
        }
        catch
        {
            // 清理阶段异常不向上抛出
        }

        _cancellationTokenSource.Dispose();
    }
}