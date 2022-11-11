namespace EasySoft.Core.EventBus.RabbitMq;

public abstract class BaseRabbitMqConsumer : IHostedService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger _logger;

    protected BaseRabbitMqConsumer(IRabbitMqConnection rabbitMqConnection, ILogger logger)
    {
        _connection = rabbitMqConnection.Connection;
        _channel = _connection.CreateModel();
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Register();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        DeRegister();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 注册消费者
    /// </summary>
    protected virtual void Register()
    {
        //获取交换机配置
        var exchange = GetExchageConfig();

        //获取routingKeys
        var routingKeys = GetRoutingKeys();

        //获取队列配置
        var queue = GetQueueConfig();

        //声明死信交换与队列
        RegiterDeadExchange(exchange.DeadExchangeName, queue.DeadQueueName, routingKeys, queue.Durable);

        //声明交换机
        _channel.ExchangeDeclare(exchange.Name, exchange.Type.ToString().ToLower());

        //声明队列
        _channel.QueueDeclare(queue.Name
            , queue.Durable
            , queue.Exclusive
            , queue.AutoDelete
            , queue.Arguments
        );

        //将队列与交换机进行绑定
        if (routingKeys.Length == 0)
            _channel.QueueBind(queue.Name, exchange.Name, string.Empty);
        else
            foreach (var key in routingKeys)
                _channel.QueueBind(queue.Name, exchange.Name, key);

        var consumer = new EventingBasicConsumer(_channel);

        //关闭自动确认,开启手动确认后需要配置这些
        if (!queue.AutoAck)
        {
            _channel.BasicQos(0, queue.PrefetchCount, queue.Global);
            _channel.BasicConsume(queue.Name, consumer: consumer, autoAck: queue.AutoAck);
        }

        consumer.Received += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var result = await Process(ea.Exchange, ea.RoutingKey, message);

            _logger.LogDebug("result:{Result},message:{Message}", result, message);

            //关闭自动确认,开启手动确认后需要依据处理结果选择返回确认信息。
            if (queue.AutoAck) return;

            if (result)
                _channel.BasicAck(ea.DeliveryTag, queue.AckMultiple);
            else
                _channel.BasicReject(ea.DeliveryTag, queue.RejectRequeue);
        };
    }

    /// <summary>
    /// 注销/关闭连接
    /// </summary>
    protected virtual void DeRegister()
    {
        _channel.Dispose();

        _connection.Dispose();
    }

    /// <summary>
    /// 获取交互机列配置
    /// </summary>
    /// <returns></returns>
    protected abstract ExchangeConfig GetExchageConfig();

    /// <summary>
    /// 获取路由keys
    /// </summary>
    /// <returns></returns>
    protected abstract string[] GetRoutingKeys();

    /// <summary>
    /// 获取队列配置
    /// </summary>
    /// <returns></returns>
    protected abstract QueueConfig GetQueueConfig();

    /// <summary>
    /// 获取队列公共配置
    /// </summary>
    /// <returns></returns>
    protected QueueConfig GetCommonQueueConfig()
    {
        return new QueueConfig()
        {
            Name = string.Empty,
            AutoDelete = false,
            Durable = false,
            Exclusive = false,
            Global = true,
            AutoAck = false,
            AckMultiple = false,
            PrefetchCount = 1,
            RejectRequeue = false,
            Arguments = null
        };
    }

    /// <summary>
    /// 处理消息的方法
    /// </summary>
    /// <param name="routingKey"></param>
    /// <param name="message"></param>
    /// <param name="exchange"></param>
    /// <returns></returns>
    protected abstract Task<bool> Process(string exchange, string routingKey, string message);

    /// <summary>
    /// 声明死信交换与队列  
    /// </summary>
    protected virtual void RegiterDeadExchange(string deadExchangeName, string deadQueueName, string[] routingKeys,
        bool durable)
    {
        if (string.IsNullOrWhiteSpace(deadExchangeName)) return;

        _channel.ExchangeDeclare(deadExchangeName, ExchangeType.Direct.ToString().ToLower());

        _channel.QueueDeclare(
            deadQueueName,
            durable,
            false,
            false,
            null
        );

        foreach (var key in routingKeys)
            _channel.QueueBind(deadQueueName, deadExchangeName, key);
    }
}