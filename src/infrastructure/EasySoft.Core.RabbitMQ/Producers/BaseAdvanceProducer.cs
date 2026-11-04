using EasySoft.Core.RabbitMQ.Channels;
using EasySoft.Core.RabbitMQ.Exchanges;
using EasySoft.Core.RabbitMQ.ExtensionMethods;
using EasySoft.Core.RabbitMQ.Queues;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.RabbitMQ.Producers;

public abstract class BaseAdvanceProducer
{
    #region Fields

    private readonly AdvanceChannelPool _channelPool;

    private readonly ILogger<BaseAdvanceProducer> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// 构造注入
    /// </summary>
    /// <param name="channelPool">连接池</param>
    /// <param name="logger">日志</param>
    protected BaseAdvanceProducer(
        AdvanceChannelPool channelPool,
        ILogger<BaseAdvanceProducer> logger
    )
    {
        _channelPool = channelPool;
        _logger = logger;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 直接队列发布
    /// </summary>
    /// <typeparam name="TMessage">队列名</typeparam>
    /// <param name="queueConfig">队列配置</param>
    /// <param name="message">消息</param>
    protected async Task BasePublishToQueueAsync<TMessage>(QueueConfig queueConfig, TMessage message)
    {
        _logger.LogInformation("PushMessage,QueueName:{QueueName}", queueConfig.GetAssembledName());

        var channel = await _channelPool.RentChannelAsync();

        var content = message as string;

        if (content == null && message != null)
        {
            content = JsonConvertAssist.SerializeObject(message);
        }

        if (content == null)
        {
            return;
        }

        var body = Encoding.UTF8.GetBytes(content);

        var props = new BasicProperties();

        await channel.BasicPublishAsync(
            string.Empty,
            queueConfig.GetAssembledName(),
            false,
            props,
            body
        );
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
    protected async Task BasePublishToExchangeAsync<TMessage>(
        ExchangeConfig exchangeConfig,
        string routingKey,
        TMessage message,
        BasicProperties properties = null,
        bool mandatory = false
    )
    {
        var channel = await _channelPool.RentChannelAsync();

        await BasePublishToExchangeCoreAsync(
            channel,
            exchangeConfig.GetAssembledName(),
            routingKey,
            message,
            properties ?? new BasicProperties(),
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
    protected virtual async Task BasePublishToExchangeWithRetryAsync<TMessage>(
        ExchangeConfig exchangeConfig,
        string routingKey,
        TMessage message,
        BasicProperties properties = null,
        bool mandatory = false
    )
    {
        var retryPolicy = Policy.Handle<SocketException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetryAsync(
                3,
                _ => TimeSpan.FromSeconds(1),
                (
                    ex,
                    timespan,
                    retryCount,
                    _
                ) =>
                {
                    _logger.LogError(
                        ex,
                        "RabbitMQ发布失败，准备第{RetryCount}次重试，等待{Delay}s | {ErrorMessage}",
                        retryCount,
                        timespan.TotalSeconds,
                        ex.Message
                    );
                }
            );

        var channel = await retryPolicy.ExecuteAsync(
            async _ => await _channelPool.RentChannelAsync(),
            null
        );

        await BasePublishToExchangeCoreAsync(
            channel,
            exchangeConfig.GetAssembledName(),
            routingKey,
            message,
            properties ?? new BasicProperties(),
            mandatory
        );
    }

    /// <summary>
    /// 通过交换机发布消息
    /// </summary>
    /// <param name="channel">通道</param>
    /// <param name="exchangeName">交换机名称</param>
    /// <param name="routingKey">路由键</param>
    /// <param name="message">消息</param>
    /// <param name="properties">消息属性</param>
    /// <param name="mandatory">消息行为控制</param>
    /// <returns></returns>
    private async Task BasePublishToExchangeCoreAsync<TMessage>(
        IChannel channel,
        string exchangeName,
        string routingKey,
        TMessage message,
        BasicProperties properties = null,
        bool mandatory = false
    )
    {
        if (string.IsNullOrWhiteSpace(exchangeName))
        {
            throw new ArgumentNullException($"参数 {nameof(exchangeName)} 不能为空");
        }

        if (string.IsNullOrWhiteSpace(routingKey))
        {
            throw new ArgumentNullException($"参数 {nameof(routingKey)} 不能为空");
        }

        if (channel == null)
        {
            throw new ArgumentNullException($"参数 {nameof(channel)} 不能为空");
        }

        var content = message as string;

        if (content == null && message != null)
        {
            content = JsonConvertAssist.SerializeObject(message);
        }

        if (content == null)
        {
            return;
        }

        var body = Encoding.UTF8.GetBytes(content);

        //当mandatory标志位设置为true时，如果exchange根据自身类型和消息routingKey无法找到一个合适的queue存储消息
        //那么broker会调用basic.return方法将消息返还给生产者;
        //当mandatory设置为false时，出现上述情况broker会直接将消息丢弃

        await channel.BasicPublishAsync(
            exchangeName,
            routingKey,
            mandatory,
            properties ?? new BasicProperties(),
            body
        );

        //开启发布消息确认模式
        //_channel.ConfirmSelect();
        //消息是否到达服务器
        //bool publishStatus = _channel.WaitForConfirms();
    }

    #endregion
}