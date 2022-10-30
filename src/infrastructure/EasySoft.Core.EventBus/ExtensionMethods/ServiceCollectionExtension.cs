using DotNetCore.CAP;
using EasySoft.Core.EventBus.Cap;
using EasySoft.Core.EventBus.Cap.Filters;
using EasySoft.Core.EventBus.Configurations;
using EasySoft.Core.EventBus.RabbitMq;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
                .Configure<RabbitMqOptions>(rabbitmqSection)
                .AddSingleton<IRabbitMqConnection>(provider =>
                {
                    var options = provider.GetRequiredService<IOptions<RabbitMqOptions>>();
                    var logger = provider.GetRequiredService<ILogger<RabbitMqConnection>>();
                    var serviceInfo = services.GetServiceInfo();
                    var clientProvidedName = serviceInfo.Id;

                    return RabbitMqConnection.GetInstance(options, clientProvidedName, logger);
                })
                .AddSingleton<RabbitMqProducer>()
            ;
    }
}