using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.RabbitMQ.Connections;
using EasySoft.Core.RabbitMQ.Producers;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.RabbitMQ.Extensions.DependencyInjection.ExtensionMethods;

public static class ServiceCollectionExtension
{
    private const string UniqueIdentifierAddCapEventBus = "852de31f-db7e-4b99-abf7-d50782f8f3da";

    public static IServiceCollection AddCapEventBus(
        this IServiceCollection services
    )
    {
        if (services.HasRegistered(UniqueIdentifierAddCapEventBus))
        {
            return services;
        }

        services.AddSingleton<IConnectionFactory>(_ =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = RabbitMQConfigAssist.GetHostName(),
                    UserName = RabbitMQConfigAssist.GetUserName(),
                    Password = RabbitMQConfigAssist.GetPassword(),
                    Port = RabbitMQConfigAssist.GetPort(),
                    VirtualHost = RabbitMQConfigAssist.GetVirtualHost(),
                    RequestedConnectionTimeout = new TimeSpan(RabbitMQConfigAssist.GetConnectionTimeout())
                };

                return factory;
            }
        );

        return services
            .AddSingleton<IAdvanceConnection, AdvanceConnection>()
            .AddHostedService<AdvanceConnection>()
            .AddSingleton<BaseAdvanceProducer>();
    }
}