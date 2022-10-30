using System.Text;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;

namespace EasySoft.Core.EventBus.RabbitMq;

public class RabbitMqProducer : IDisposable
{
    private readonly IModel _channel;
    private readonly ILogger<RabbitMqProducer> _logger;

    public RabbitMqProducer(IRabbitMqConnection rabbitMqConnection, ILogger<RabbitMqProducer> logger)
    {
        _logger = logger;
        _channel = rabbitMqConnection.Connection.CreateModel();
    }

    /// <summary>
    /// 简单队列,不通过交换机
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <param name="exchange"></param>
    /// <param name="routingKey"></param>
    /// <param name="message"></param>
    /// <param name="properties"></param>
    /// <param name="mandatory"></param>
    //public virtual void BasicPublish<TMessage>(string queueName, TMessage message)
    //{
    //    _logger.LogInformation($"PushMessage,queueName:{queueName}");

    //    _channel.QueueDeclare(queue: queueName,
    //                          durable: false,
    //                          exclusive: false,
    //                          autoDelete: false,
    //                          arguments: null);

    //    var content = JsonSerializer.Serialize(message);
    //    var body = Encoding.UTF8.GetBytes(content);

    //    _channel.BasicPublish(exchange: string.Empty,
    //                          routingKey: queueName,
    //                          basicProperties: null,
    //                          body: body);
    //}
    public virtual void BasicPublish<TMessage>(
        string exchange,
        string routingKey,
        TMessage message,
        IBasicProperties? properties = null,
        bool mandatory = false
    )
    {
        Policy.Handle<Exception>()
            .WaitAndRetry(
                3,
                _ => TimeSpan.FromSeconds(1),
                (ex, _, retryCount, _) =>
                {
                    _logger.LogError(ex, "{RetryCount}:{Arg1Message}", retryCount, ex.Message);
                }
            )
            .Execute(() =>
            {
                var content = message as string;

                if (content == null && message != null) content = JsonConvertAssist.Serialize(message);

                if (content == null) return;

                var body = Encoding.UTF8.GetBytes(content);

                //当mandatory标志位设置为true时，如果exchange根据自身类型和消息routingKey无法找到一个合适的queue存储消息
                //那么broker会调用basic.return方法将消息返还给生产者;
                //当mandatory设置为false时，出现上述情况broker会直接将消息丢弃

                _channel.BasicPublish(exchange, routingKey, mandatory, properties, body);

                //开启发布消息确认模式
                //_channel.ConfirmSelect();
                //消息是否到达服务器
                //bool publishStatus = _channel.WaitForConfirms();
            });
    }

    public virtual IBasicProperties CreateBasicProperties()
    {
        return _channel.CreateBasicProperties();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) _channel.Dispose();
    }
}