using DotNetCore.CAP;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EventBus.Cap;
using EasySoft.Core.EventBus.Cap.Filters;
using EasySoft.Core.EventBus.RabbitMq;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.EventBus.ExtensionMethods;

public static class ServiceCollectionExtension
{
    private const string UniqueIdentifierAddCapEventBus = "852de31f-db7e-4b99-abf7-d50782f8f3da";

    public static IServiceCollection AddCapEventBus<TSubscriber>(
        this IServiceCollection services,
        Action<CapOptions> setupAction
    ) where TSubscriber : class, ICapSubscribe
    {
        if (services.HasRegistered(UniqueIdentifierAddCapEventBus))
            return services;

        services
            .AddSingleton<IEventPublisher, CapPublisher>()
            .AddScoped<TSubscriber>()
            .AddCap(setupAction)
            .AddSubscribeFilter<DefaultCapFilter>();

        return services;
    }

    public static IServiceCollection AddCapEventBus(
        this IServiceCollection services,
        IConfigurationSection rabbitmqSection
    )
    {
        if (services.HasRegistered(UniqueIdentifierAddCapEventBus))
            return services;

        return services
            .AddSingleton<IRabbitMqConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger>();
                var clientProvidedName = GeneralConfigAssist.GetId();

                return RabbitMqConnection.GetInstance(clientProvidedName, logger);
            })
            .AddSingleton<RabbitMqProducer>();
    }
}