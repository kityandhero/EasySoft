using EasySoft.Core.RabbitMQ.Channels;
using EasySoft.Core.RabbitMQ.Exchanges;
using EasySoft.Core.RabbitMQ.ExtensionMethods;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace EasySoft.Core.RabbitMQ.Consumers;

public abstract class BaseExchangeAdvanceConsumer : BaseAdvanceConsumer
{
    #region Constructors

    /// <summary>
    /// 构造注入
    /// </summary>
    /// <param name="channelPool">连接池</param>
    /// <param name="logger">日志</param>
    protected BaseExchangeAdvanceConsumer(
        AdvanceChannelPool channelPool,
        ILogger<BaseExchangeAdvanceConsumer> logger
    ) : base(
        channelPool,
        logger
    )
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// 获取交互配置
    /// </summary>
    /// <returns></returns>
    protected abstract ExchangeConfig GetExchangeConfig();

    /// <summary>
    /// 获取路由keys
    /// </summary>
    /// <returns></returns>
    protected abstract string GetRoutingKey();

    /// <summary>
    /// 准备死信处理
    /// </summary>
    protected override async Task PrepareBusiness(IChannel channel, CancellationToken cancellationToken)
    {
        var exchangeConfig = GetExchangeConfig();

        var queue = GetQueueConfig();

        await channel.ExchangeDeclareAsync(
            exchangeConfig.GetAssembledName(),
            ExchangeType.Direct,
            queue.Durable,
            queue.AutoDelete,
            null,
            false,
            false,
            cancellationToken
        );

        await channel.QueueBindAsync(
            queue.GetAssembledName(),
            exchangeConfig.GetAssembledName(),
            GetRoutingKey(),
            null,
            false,
            cancellationToken
        );
    }

    #endregion
}