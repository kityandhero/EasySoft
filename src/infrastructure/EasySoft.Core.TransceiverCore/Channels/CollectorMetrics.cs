namespace EasySoft.Core.TransceiverCore.Channels;

/// <summary>
/// 收集器整体指标快照，聚合主通道与全部二级输出通道指标，用于全局观测、水位告警、流量排查
/// </summary>
public readonly struct CollectorMetrics
{
    /// <summary>
    /// 主通道当前排队消息数
    /// <para>备注：由主Channel.Reader.Count获取，仅为近似值，非强一致精确计数；用于观测上游入队水位、判断是否触发主通道泄洪，不适合对账。</para>
    /// </summary>
    public int MainQueued { get; }

    /// <summary>
    /// 文件输出通道指标快照
    /// </summary>
    public SinkMetrics FileSink { get; }

    /// <summary>
    /// MQ消息输出通道指标快照
    /// </summary>
    public SinkMetrics MqSink { get; }

    /// <summary>
    /// 收集器整体指标快照，聚合主通道与全部二级输出通道指标，用于全局观测、水位告警、流量排查
    /// </summary>
    /// <param name="mainQueued">主通道当前排队近似数量</param>
    /// <param name="fileSink">文件输出通道指标快照</param>
    /// <param name="mqSink">MQ输出通道指标快照</param>
    public CollectorMetrics(int mainQueued, SinkMetrics fileSink, SinkMetrics mqSink)
    {
        MainQueued = mainQueued;
        FileSink = fileSink;
        MqSink = mqSink;
    }
}