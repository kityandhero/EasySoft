using EasySoft.Core.RabbitMQ.Channels;
using EasySoft.Core.RabbitMQ.Exchanges;
using EasySoft.Core.RabbitMQ.Queues;

namespace EasySoft.Core.RabbitMQ.Producers;

/// <summary>
/// 泛型化基础发布器
/// </summary>
public abstract class BaseGenericAdvanceProducer : BaseAdvanceProducer
{
    #region Constructors

    /// <summary>
    /// 泛型化基础发布器
    /// </summary>
    /// <param name="channelPool"></param>
    /// <param name="logger"></param>
    protected BaseGenericAdvanceProducer(
        AdvanceChannelPool channelPool,
        ILogger<BaseGenericAdvanceProducer> logger
    ) : base(channelPool, logger)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// 获取队列配置
    /// </summary>
    /// <returns></returns>
    protected abstract QueueConfig GetQueueConfig();

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
    /// 直接队列发布
    /// </summary>
    /// <typeparam name="TMessage">队列名</typeparam>
    /// <param name="message">消息</param>
    public async Task PublishToQueueAsync<TMessage>(TMessage message)
    {
        await BasePublishToQueueAsync(GetQueueConfig(), message);
    }

    /// <summary>
    /// 通过交换机发布消息
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="properties">消息属性</param>
    /// <param name="mandatory">消息行为控制</param>
    /// <typeparam name="TMessage">消息类型</typeparam>
    /// <returns></returns>
    public async Task PublishToExchangeAsync<TMessage>(
        TMessage message,
        BasicProperties properties = null,
        bool mandatory = false
    )
    {
        var exchangeConfig = GetExchangeConfig();

        await BasePublishToExchangeAsync(
            exchangeConfig,
            GetRoutingKey(),
            message,
            properties,
            mandatory
        );
    }

    /// <summary>
    /// 通过交换机发布
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="properties">消息属性</param>
    /// <param name="mandatory">消息行为控制</param>
    /// <typeparam name="TMessage">消息类型</typeparam>
    protected virtual async Task PublishToExchangeWithRetryAsync<TMessage>(
        TMessage message,
        BasicProperties properties = null,
        bool mandatory = false
    )
    {
        var exchangeConfig = GetExchangeConfig();

        await BasePublishToExchangeWithRetryAsync(
            exchangeConfig,
            GetRoutingKey(),
            message,
            properties,
            mandatory
        );
    }

    #endregion
}