using DotNetCore.CAP;
using DotNetCore.CAP.Filter;
using EasySoft.Core.Cap.Persistent;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Cap.Transport.ExtensionMethods.RabbitMQ;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdvanceCapRabbitMQ(
        this IServiceCollection serviceCollection,
        PersistentType persistentType
    )
    {
        serviceCollection.AddCap(options =>
        {
            options.UseAdvanceCapRabbitMQ();

            options.UsePersistent(persistentType, RabbitMQConfigAssist.GetPersistentConnection());
        });

        return serviceCollection;
    }

    public static IServiceCollection AddAdvanceCapRabbitMQ(
        this IServiceCollection serviceCollection,
        Action<RabbitMQOptions> action,
        PersistentType persistentType
    )
    {
        serviceCollection.AddCap(options =>
        {
            options.UseAdvanceCapRabbitMQ(action);

            options.UsePersistent(persistentType, RabbitMQConfigAssist.GetPersistentConnection());
        });

        return serviceCollection;
    }

    public static IServiceCollection AddAdvanceCapRabbitMQ<T>(
        this IServiceCollection serviceCollection,
        PersistentType persistentType
    ) where T : SubscribeFilter
    {
        serviceCollection.AddCap(options =>
            {
                options.UseAdvanceCapRabbitMQ();

                options.UsePersistent(persistentType, RabbitMQConfigAssist.GetPersistentConnection());
            })
            .AddSubscribeFilter<T>();

        return serviceCollection;
    }

    public static IServiceCollection AddAdvanceCapRabbitMQ<T>(
        this IServiceCollection serviceCollection,
        Action<RabbitMQOptions> action,
        PersistentType persistentType
    ) where T : SubscribeFilter
    {
        serviceCollection.AddCap(options =>
            {
                options.UseAdvanceCapRabbitMQ(action);

                options.UsePersistent(persistentType, RabbitMQConfigAssist.GetPersistentConnection());
            })
            .AddSubscribeFilter<T>();

        return serviceCollection;
    }

    private static CapOptions UseAdvanceCapRabbitMQ(
        this CapOptions capOptions
    )
    {
        var prefix = RabbitMQConfigAssist.GetPrefix().Remove(" ").Trim().ToLower();

        capOptions.UseRabbitMQ(rabbitMQOptions =>
        {
            rabbitMQOptions.HostName = RabbitMQConfigAssist.GetHostName();
            rabbitMQOptions.UserName = RabbitMQConfigAssist.GetUserName();
            rabbitMQOptions.Password = RabbitMQConfigAssist.GetPassword();
            rabbitMQOptions.VirtualHost = RabbitMQConfigAssist.GetVirtualHost();
            rabbitMQOptions.ConnectionFactoryOptions = o =>
            {
                o.RequestedConnectionTimeout = TimeSpan.FromSeconds(
                    RabbitMQConfigAssist.GetConnectionTimeout()
                );
            };

            rabbitMQOptions.ExchangeName = $"cap.{(string.IsNullOrWhiteSpace(prefix) ? "default" : prefix)}.topic";
        });

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            capOptions.GroupNamePrefix = prefix;
        }

        return capOptions;
    }

    private static CapOptions UseAdvanceCapRabbitMQ(
        this CapOptions capOptions,
        Action<RabbitMQOptions> action
    )
    {
        var prefix = RabbitMQConfigAssist.GetPrefix().Remove(" ").Trim().ToLower();

        capOptions.UseRabbitMQ(rabbitMQOptions =>
        {
            rabbitMQOptions.HostName = RabbitMQConfigAssist.GetHostName();
            rabbitMQOptions.UserName = RabbitMQConfigAssist.GetUserName();
            rabbitMQOptions.Password = RabbitMQConfigAssist.GetPassword();
            rabbitMQOptions.VirtualHost = RabbitMQConfigAssist.GetVirtualHost();
            rabbitMQOptions.ConnectionFactoryOptions = o =>
            {
                o.RequestedConnectionTimeout = TimeSpan.FromSeconds(
                    RabbitMQConfigAssist.GetConnectionTimeout()
                );
            };

            rabbitMQOptions.ExchangeName = $"cap.{(string.IsNullOrWhiteSpace(prefix) ? "default" : prefix)}.topic";

            action(rabbitMQOptions);
        });

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            capOptions.GroupNamePrefix = prefix;
        }

        return capOptions;
    }
}