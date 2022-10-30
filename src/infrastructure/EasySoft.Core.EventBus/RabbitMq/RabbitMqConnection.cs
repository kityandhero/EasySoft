using System.Net.Sockets;
using EasySoft.Core.EventBus.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace EasySoft.Core.EventBus.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection
{
    private static volatile RabbitMqConnection? _uniqueInstance;
    private static readonly object LockObject = new();
    public IConnection Connection { get; private set; } = default!;

    private RabbitMqConnection()
    {
    }

    internal static RabbitMqConnection GetInstance(IOptions<RabbitMqOptions> options, string clientProvidedName,
        ILogger<dynamic> logger)
    {
        if (_uniqueInstance is null)
            lock (LockObject)
            {
                if (_uniqueInstance is null)
                    _uniqueInstance = new RabbitMqConnection(options, clientProvidedName, logger);
            }

        return _uniqueInstance;
    }

    private RabbitMqConnection(IOptions<RabbitMqOptions> options, string clientProvidedName, ILogger logger)
    {
        var factory = new ConnectionFactory()
        {
            ClientProvidedName = clientProvidedName,
            HostName = options.Value.HostName,
            VirtualHost = options.Value.VirtualHost,
            UserName = options.Value.UserName,
            Password = options.Value.Password,
            Port = options.Value.Port,
            //Rabbitmq集群必需加这两个参数
            AutomaticRecoveryEnabled = true
            //TopologyRecoveryEnabled=true
        };

        Policy.Handle<SocketException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetry(
                2,
                retryAttempt => TimeSpan.FromSeconds(1),
                (ex, time, retryCount, content) =>
                {
                    if (2 == retryCount)
                        throw ex;

                    logger.LogError(ex, "{RetryCount}:{Arg1Message}", retryCount, ex.Message);
                }
            )
            .Execute(() => { Connection = factory.CreateConnection(); });
    }
}