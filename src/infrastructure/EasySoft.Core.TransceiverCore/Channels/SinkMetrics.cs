namespace EasySoft.Core.TransceiverCore.Channels;

/// <summary>
/// 单个输出通道（Sink）指标快照，用于监控、告警与流量诊断
/// </summary>
public readonly struct SinkMetrics
{
    /// <summary>
    /// 当前通道排队消息数
    /// </summary>
    /// <remarks>
    /// 由 Channel.Reader.Count 获取，仅为近似值，不具备强一致性；仅适合观测水位、做趋势判断，不可用于精确对账。
    /// </remarks>
    public int CurrentQueued { get; }

    /// <summary>
    /// 通道开关状态
    /// </summary>
    /// <remarks>
    ///true=启用，转发循环会向该通道投递新消息；false=关闭，不再接收新转发消息，已有存量排队消息会继续消费直至排空。
    /// </remarks>
    public bool Enabled { get; }

    /// <summary>
    /// 进入该子通道的消息总数（转发成功写入子通道）
    /// </summary>
    /// <remarks>
    ///统计口径：WriteToSink 内 TryWrite 返回 true 时计数；不包含被开关拦截、未投递进入子通道的消息。
    /// </remarks>
    public long SubIn { get; }

    /// <summary>
    /// 该通道消费成功的消息总数
    /// </summary>
    /// <remarks>
    /// 统计口径：一批消息业务处理完成后批量累加；一条消息只计一次成功。
    /// </remarks>
    public long SubOut { get; }

    /// <summary>
    /// 子通道满、触发泄洪策略丢弃的消息数量
    /// </summary>
    /// <remarks>
    ///策略：BoundedChannelFullMode.DropOldest，通道写满时丢弃旧消息，计入本指标，代表下游消费跟不上转发速率。
    /// </remarks>
    public long SubEvict { get; }

    /// <summary>
    /// 组件 Close/Dispose 阶段，通道内未消费、直接丢弃的消息数量
    /// </summary>
    /// <remarks>
    ///契约说明：本组件为内存缓冲设计，进程关闭时不持久化滞留消息，该丢弃行为是设计默许的。
    /// </remarks>
    public long SubTermDrop { get; }

    /// <summary>
    /// 下游发送失败次数（IO异常、网络异常、超时等业务侧异常）
    /// </summary>
    /// <remarks>
    ///仅统计业务抽象（WriteFileAsync / TransferAsync）抛出异常的场景；框架层面泄洪丢弃不计入此字段。
    /// </remarks>
    public long PublishFail { get; }

    /// <param name="currentQueued">当前排队近似数量</param>
    /// <param name="enabled">通道启用开关状态</param>
    /// <param name="subIn">投递进入子通道总条数</param>
    /// <param name="subOut">消费成功总条数</param>
    /// <param name="subEvict">通道满泄洪丢弃条数</param>
    /// <param name="subTermDrop">关闭阶段剩余消息丢弃条数</param>
    /// <param name="publishFail">下游发送失败次数</param>
    public SinkMetrics(
        int currentQueued,
        bool enabled,
        long subIn,
        long subOut,
        long subEvict,
        long subTermDrop,
        long publishFail
    )
    {
        CurrentQueued = currentQueued;
        Enabled = enabled;
        SubIn = subIn;
        SubOut = subOut;
        SubEvict = subEvict;
        SubTermDrop = subTermDrop;
        PublishFail = publishFail;
    }
}