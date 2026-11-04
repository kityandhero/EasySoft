using System.Threading.Channels;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Options;

/// <summary>
/// 内存队列版本的配置。
/// </summary>
public sealed class InMemoryInformationCollectorOptions
{
    /// <summary>
    /// 队列容量上限。Channel 的 BoundedCapacity，达到上限后行为由 <see cref="FullMode"/> 决定。
    /// 这是内存队列版本唯一的“数量限制”入口。
    /// </summary>
    public int Capacity { get; set; } = 1000;

    /// <summary>
    /// 队列满时的策略。
    /// <list type="bullet">
    /// <item><description><c>DropWrite</c>（默认）：丢弃当前这条新消息，队列保持原样。静默丢弃，符合内存队列“尽力而为”的定位。</description></item>
    /// <item><description><c>DropOldest</c>：丢弃队列头部最旧的消息，腾出位置给新消息。</description></item>
    /// <item><description><c>Wait</c>：阻塞发布者直到有空位。不丢消息，但会拖慢调用方，与内存队列的异步解耦定位有张力，通常不用。</description></item>
    /// </list>
    /// </summary>
    public BoundedChannelFullMode FullMode { get; set; } = BoundedChannelFullMode.DropOldest;
}