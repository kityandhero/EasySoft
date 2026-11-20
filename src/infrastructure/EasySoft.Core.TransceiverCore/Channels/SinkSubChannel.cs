namespace EasySoft.Core.TransceiverCore.Channels;

/// <summary>
/// 二级输出Sink子通道单元，封装独立通道、消费任务、全链路指标
/// </summary>
public sealed class SinkSubChannel
{
    /// <summary>
    /// Channel消息队列实例
    /// </summary>
    public Channel<MessagePacket> Channel { get; set; } = default!;

    /// <summary>
    /// 该通道专属消费循环任务句柄
    /// </summary>
    public Task ConsumerTask { get; set; } = default!;

    /// <summary>
    /// 启用标记底层存储，1=启用，0=禁用，仅通过Interlocked原子操作读写，无锁并发安全
    /// </summary>
    private int _enabledFlag;

    /// <summary>
    /// 成功投递进入本二级通道的消息总数
    /// </summary>
    public long SubIn;

    /// <summary>
    /// 下游业务处理成功的消息总数
    /// </summary>
    public long SubOut;

    /// <summary>
    /// 通道写满触发泄洪，被丢弃的消息总数（DropOldest）
    /// </summary>
    public long SubEvict;

    /// <summary>
    /// 组件Close/Dispose阶段，通道内未消费被丢弃的消息总数
    /// </summary>
    public long SubTermDrop;

    /// <summary>
    /// 下游业务执行异常计数（IO、网络、超时等业务侧异常）
    /// </summary>
    public long PublishFail;

    /// <summary>
    /// 通道启用开关，原子读写，线程安全
    /// <para>关闭：转发侧不再投递新消息到此通道；存量排队消息继续消费</para>
    /// </summary>
    public bool Enabled
    {
        get => Interlocked.CompareExchange(
            ref _enabledFlag,
            0,
            0
        ) == 1;
        set
        {
            if (value)
            {
                Interlocked.Exchange(ref _enabledFlag, 1);
            }
            else
            {
                Interlocked.Exchange(ref _enabledFlag, 0);
            }
        }
    }

    /// <summary>
    /// 快照生成当前通道指标
    /// </summary>
    /// <returns>Sink指标只读快照</returns>
    public SinkMetrics Snapshot()
    {
        return new SinkMetrics(
            Channel.Reader.Count,
            Enabled,
            SubIn,
            SubOut,
            SubEvict,
            SubTermDrop,
            PublishFail
        );
    }
}