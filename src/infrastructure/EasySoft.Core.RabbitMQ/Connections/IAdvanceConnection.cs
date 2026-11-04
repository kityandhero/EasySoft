namespace EasySoft.Core.RabbitMQ.Connections;

public interface IAdvanceConnection
{
    IConnection Connection { get; }
}