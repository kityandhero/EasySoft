using EasySoft.Core.RabbitMQ.Channels;
using EasySoft.Core.RabbitMQ.Exchanges;
using EasySoft.Core.RabbitMQ.Queues;

namespace EasySoft.Core.RabbitMQ.Producers;

/// <summary>
/// 一般性发布器
/// </summary>
public class GeneralAdvanceProducer : BaseAdvanceProducer
{
    #region Constructors

    /// <summary>
    /// 一般性发布器
    /// </summary>
    /// <param name="channelPool"></param>
    /// <param name="logger"></param>
    public GeneralAdvanceProducer(
        AdvanceChannelPool channelPool,
        ILogger<GeneralAdvanceProducer> logger
    ) : base(
        channelPool,
        logger
    )
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// 直接队列发布
    /// </summary>
    /// <typeparam name="TMessage">队列名</typeparam>
    /// <param name="queueConfig">队列配置</param>
    /// <param name="message">消息</param>
    public async Task PublishToQueueAsync<TMessage>(QueueConfig queueConfig, TMessage message)
    {
        await BasePublishToQueueAsync(queueConfig, message);
    }

    /// <summary>
    /// 通过交换机发布消息
    /// </summary>
    /// <param name="exchangeConfig">交换机配置</param>
    /// <param name="routingKey">路由键</param>
    /// <param name="message">消息</param>
    /// <param name="properties">消息属性</param>
    /// <param name="mandatory">消息行为控制</param>
    /// <typeparam name="TMessage">消息类型</typeparam>
    /// <returns></returns>
    public async Task PublishToExchangeAsync<TMessage>(
        ExchangeConfig exchangeConfig,
        string routingKey,
        TMessage message,
        BasicProperties? properties = null,
        bool mandatory = false
    )
    {
        await BasePublishToExchangeAsync(
            exchangeConfig,
            routingKey,
            message,
            properties,
            mandatory
        );
    }

    /// <summary>
    /// 通过交换机发布
    /// </summary>
    /// <param name="exchangeConfig">交换机配置</param>
    /// <param name="routingKey">路由键</param>
    /// <param name="message">消息</param>
    /// <param name="properties">消息属性</param>
    /// <param name="mandatory">消息行为控制</param>
    /// <typeparam name="TMessage">消息类型</typeparam>
    protected virtual async Task PublishToExchangeWithRetryAsync<TMessage>(
        ExchangeConfig exchangeConfig,
        string routingKey,
        TMessage message,
        BasicProperties properties = null,
        bool mandatory = false
    )
    {
        await BasePublishToExchangeWithRetryAsync(
            exchangeConfig,
            routingKey,
            message,
            properties,
            mandatory
        );
    }

    #endregion
}