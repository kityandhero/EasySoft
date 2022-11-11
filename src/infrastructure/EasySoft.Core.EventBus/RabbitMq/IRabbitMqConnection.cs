namespace EasySoft.Core.EventBus.RabbitMq;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}