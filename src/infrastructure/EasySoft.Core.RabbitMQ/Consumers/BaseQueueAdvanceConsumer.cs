using EasySoft.Core.RabbitMQ.Channels;

namespace EasySoft.Core.RabbitMQ.Consumers;

public abstract class BaseQueueAdvanceConsumer : BaseAdvanceConsumer
{
    #region Constructors

    /// <summary>
    /// 构造注入
    /// </summary>
    /// <param name="channelPool">连接池</param>
    /// <param name="logger">日志</param>
    protected BaseQueueAdvanceConsumer(AdvanceChannelPool channelPool, ILogger<BaseQueueAdvanceConsumer> logger) : base(
        channelPool,
        logger
    )
    {
    }

    #endregion
}