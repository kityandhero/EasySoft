using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.RabbitMQ.Channels;
using EasySoft.Core.RabbitMQ.Connections;
using EasySoft.Core.RabbitMQ.Producers;
using EasySoft.UtilityTools.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.RabbitMQ.Extensions.DependencyInjection.ExtensionMethods;

public static class ServiceCollectionExtension
{
    private const string UniqueIdentifierAddAdvanceRabbitMQ = "92e551026378474f87bf8e37eac42d5d";

    internal static WebApplicationBuilder AddAdvanceRabbitMQ(
        this WebApplicationBuilder builder,
        string clientProvidedName
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceRabbitMQ))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceRabbitMQ)}."
        );

        builder.Services.AddSingleton<IConnectionFactory>(sp =>
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
                    AutomaticRecoveryEnabled = true,

                    //TopologyRecoveryEnabled=true

                    RequestedConnectionTimeout = new TimeSpan(RabbitMQConfigAssist.GetConnectionTimeout())
                };

                return factory;
            }
        );

        // 注册连接HostedService，同时暴露IRabbitMqConnection
        builder.Services.AddSingleton<IAdvanceConnection, AdvanceConnection>();
        builder.Services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<AdvanceConnection>());

        // 通道池单例
        builder.Services.AddSingleton(serviceProvider =>
            {
                var connWrapper = serviceProvider.GetRequiredService<IAdvanceConnection>();
                var logger = serviceProvider.GetRequiredService<ILogger<AdvanceChannelPool>>();

                return new AdvanceChannelPool(
                    connWrapper,
                    logger,
                    8,
                    true,
                    true
                );
            }
        );

        // 生产者
        builder.Services.AddSingleton<BaseAdvanceProducer>();

        return builder;
    }
}