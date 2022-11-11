using System.Net.Sockets;
using EasySoft.Core.Config.ConfigAssist;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace EasySoft.Core.EventBus.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection
{
    private static volatile RabbitMqConnection? _uniqueInstance;
    private static readonly object LockObject = new();
    public IConnection Connection { get; private set; } = default!;

    internal static RabbitMqConnection GetInstance(
        string clientProvidedName,
        ILogger logger
    )
    {
        if (_uniqueInstance is not null) return _uniqueInstance;

        lock (LockObject)
        {
            if (_uniqueInstance is not null) return _uniqueInstance;

            _uniqueInstance = new RabbitMqConnection(clientProvidedName, logger);
        }

        return _uniqueInstance;
    }

    private RabbitMqConnection(string clientProvidedName, ILogger logger)
    {
        var factory = new ConnectionFactory
        {
            ClientProvidedName = clientProvidedName,
            HostName = RabbitMQConfigAssist.GetHostName(),
            VirtualHost = RabbitMQConfigAssist.GetVirtualHost(),
            UserName = RabbitMQConfigAssist.GetUserName(),
            Password = RabbitMQConfigAssist.GetPassword(),
            Port = RabbitMQConfigAssist.GetPort(),
            //Rabbitmq集群必需加这两个参数
            AutomaticRecoveryEnabled = true
            //TopologyRecoveryEnabled=true
        };

        Policy.Handle<SocketException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetry(
                2,
                _ => TimeSpan.FromSeconds(1),
                (ex, _, retryCount, _) =>
                {
                    if (2 == retryCount)
                        throw ex;

                    logger.LogError(ex, "{RetryCount}:{Arg1Message}", retryCount, ex.Message);
                }
            )
            .Execute(() => { Connection = factory.CreateConnection(); });
    }
}