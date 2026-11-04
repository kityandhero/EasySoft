using EasySoft.Core.RabbitMQ.Channels;
using EasySoft.Core.RabbitMQ.ExtensionMethods;
using EasySoft.Core.RabbitMQ.Queues;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace EasySoft.Core.RabbitMQ.Consumers;

public abstract class BaseAdvanceConsumer : BackgroundService
{
    #region Fields

    private readonly AdvanceChannelPool _channelPool;

    private readonly ILogger<BaseAdvanceConsumer> _logger;

    private AsyncEventingBasicConsumer _consumer;

    // polly本地重试策略
    private AsyncRetryPolicy _localRetryPolicy;

    #endregion

    #region Constructors

    /// <summary>
    /// 构造注入
    /// </summary>
    /// <param name="channelPool">连接池</param>
    /// <param name="logger">日志</param>
    protected BaseAdvanceConsumer(
        AdvanceChannelPool channelPool,
        ILogger<BaseAdvanceConsumer> logger
    )
    {
        _channelPool = channelPool;
        _logger = logger;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 获取队列配置
    /// </summary>
    /// <returns></returns>
    protected abstract QueueConfig GetQueueConfig();

    /// <summary>
    /// 准备业务处理
    /// </summary>
    protected virtual Task PrepareBusiness(IChannel channel, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 准备死信处理
    /// </summary>
    private async Task PrepareDead(IChannel channel, CancellationToken cancellationToken)
    {
        var queue = GetQueueConfig();

        // 声明业务队列
        await channel.QueueDeclareAsync(
            queue.GetAssembledName(),
            queue.Durable,
            queue.Exclusive,
            queue.AutoDelete,
            queue.GetAssembledArguments(),
            false,
            false,
            cancellationToken
        );

        // 声明死信交换机
        await channel.ExchangeDeclareAsync(
            queue.GetAssembledDeadExchangeName(),
            ExchangeType.Direct,
            queue.Durable,
            queue.AutoDelete,
            null,
            false,
            false,
            cancellationToken
        );

        // 声明死信队列
        await channel.QueueDeclareAsync(
            queue.GetAssembledDeadQueueName(),
            queue.Durable,
            queue.Exclusive,
            queue.AutoDelete,
            null,
            false,
            false,
            cancellationToken
        );

        // 死信队列绑定死信交换机
        await channel.QueueBindAsync(
            queue.GetAssembledDeadQueueName(),
            queue.GetAssembledDeadExchangeName(),
            queue.GetAssembledDeadRoutingKey(),
            null,
            false,
            cancellationToken
        );
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var queue = GetQueueConfig();

        // 初始化本地重试策略
        if (!queue.WhetherRetryWhenConsumeFail)
        {
            _localRetryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    queue.LocalMaxRetryCount,
                    _ => TimeSpan.FromMilliseconds(queue.LocalRetryDelayMillisecond),
                    async (
                        ex,
                        _,
                        retryAttempt,
                        _
                    ) =>
                    {
                        _logger.LogWarning(
                            ex,
                            "【{Queue}】本地重试，当前重试次数：{RetryAttempt}, 延迟{Delay}ms",
                            queue.GetAssembledName(),
                            retryAttempt,
                            queue.LocalMaxRetryCount
                        );

                        await Task.CompletedTask;
                    }
                );
        }

        // 获取通道（通道池拿通道，长期消费通道一般单独持有，不归还回池，消费通道独占）
        var channel = await _channelPool.RentChannelAsync();

        await PrepareBusiness(channel, cancellationToken);

        await PrepareDead(channel, cancellationToken);

        // 创建消费者实例
        _consumer = new AsyncEventingBasicConsumer(channel);

        //关闭自动确认,开启手动确认后需要配置这些
        if (!queue.AutoAck)
        {
            await channel.BasicQosAsync(
                0,
                queue.PrefetchCount,
                queue.Global,
                cancellationToken
            );
        }

        await channel.BasicQosAsync(
            0,
            queue.PrefetchCount,
            false,
            cancellationToken
        );

        // 注册消费，BasicConsumeAsync
        await channel.BasicConsumeAsync(
            queue.GetAssembledName(),
            queue.AutoAck,
            _consumer,
            cancellationToken
        );

        _consumer.ReceivedAsync += async (_, e) => await OnMessageReceived(e, cancellationToken);

        _logger.LogInformation("消费者启动成功，队列:{Queue}", queue.GetAssembledName());

        // 阻塞直到应用停止
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000, cancellationToken);
        }
    }

    /// <summary>
    /// 消息接收回调  
    /// </summary>
    private async Task OnMessageReceived(
        BasicDeliverEventArgs basicDeliverEventArgs,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var queue = GetQueueConfig();

            var channel = await _channelPool.RentChannelAsync();

            if (channel == null)
            {
                return;
            }

            var body = basicDeliverEventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var deliveryTag = basicDeliverEventArgs.DeliveryTag;

            try
            {
                _logger.LogInformation("收到消息：{Msg}", message);

                // 业务处理
                await HandleMessage(message, cancellationToken);

                // 业务处理成功，手动Ack，删除队列消息
                await channel.BasicAckAsync(
                    deliveryTag,
                    queue.AckMultiple,
                    cancellationToken
                );
            }
            catch (Exception e)
            {
                _logger.LogError(
                    e,
                    "【{Queue}】消息处理异常",
                    queue.GetAssembledName()
                );

                if (queue.WhetherRetryWhenConsumeFail)
                {
                    var retryCount = 0;

                    if (basicDeliverEventArgs.BasicProperties.Headers != null &&
                        basicDeliverEventArgs.BasicProperties.Headers.TryGetValue("x-retry-count", out var obj))
                    {
                        retryCount = Convert.ToInt32(obj);
                    }

                    if (retryCount < queue.RequeueMaxRetryCount)
                    {
                        retryCount++;

                        // 复制原有headers，追加重试计数
                        var newProps = new BasicProperties
                        {
                            DeliveryMode = basicDeliverEventArgs.BasicProperties.DeliveryMode,

                            // 复制旧headers
                            Headers = new Dictionary<string, object>(
                                basicDeliverEventArgs.BasicProperties.Headers ?? new Dictionary<string, object>()
                            )
                        };

                        newProps.Headers["x-retry-count"] = retryCount;

                        // 重新发布回原队列（DirectQueue模式）
                        await channel.BasicPublishAsync(
                            "",
                            queue.GetAssembledName(),
                            true,
                            newProps,
                            basicDeliverEventArgs.Body,
                            cancellationToken
                        );

                        // 由于 新消息已经入队，旧消息直接Ack，不再requeue, 
                        await channel.BasicAckAsync(
                            deliveryTag,
                            queue.AckMultiple,
                            cancellationToken
                        );

                        _logger.LogWarning(
                            "【{Queue}】消息重新发布重试，次数:{Retry}",
                            queue.GetAssembledName(),
                            retryCount
                        );
                    }
                    else
                    {
                        // 超过最大重试，不再重入，转入死信
                        await channel.BasicNackAsync(
                            deliveryTag,
                            queue.AckMultiple,
                            false,
                            cancellationToken
                        );

                        _logger.LogError("【{Queue}】超过最大重试，转入死信队列", queue.GetAssembledName());
                    }
                }
                else
                {
                    #region 本地Polly重试

                    try
                    {
                        await _localRetryPolicy.ExecuteAsync(async () =>
                            {
                                _logger.LogInformation(
                                    "【{Queue}】收到消息，开始处理：{Msg}",
                                    queue.GetAssembledName(),
                                    message
                                );

                                await HandleMessage(message, cancellationToken);
                            }
                        );

                        await channel.BasicAckAsync(
                            deliveryTag,
                            queue.AckMultiple,
                            cancellationToken
                        );
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(
                            ex,
                            "【{Queue}】本地重试全部耗尽，转入死信队列",
                            queue.GetAssembledName()
                        );

                        await channel.BasicNackAsync(
                            deliveryTag,
                            queue.AckMultiple,
                            false,
                            cancellationToken
                        );

                        _logger.LogError("【{Queue}】消费失败，转入死信队列", queue.GetAssembledName());
                    }

                    #endregion
                }
            }
        }
        catch (Exception e)
        {
            var message = e.Message;

            _logger.LogError("消费消息异常：{Message}", message);
        }
    }

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected abstract Task<bool> HandleMessage(string message, CancellationToken cancellationToken);

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        var channel = await _channelPool.RentChannelAsync();

        _logger.LogInformation("消费者停止");

        if (channel.IsOpen)
        {
            await channel.CloseAsync(cancellationToken);
            await channel.DisposeAsync();
        }

        await base.StopAsync(cancellationToken);
    }

    #endregion
}