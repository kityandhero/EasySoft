using EasySoft.Core.EventBus.Cap;
using EasySoft.Core.EventBus.Cap.Filters;
using EasySoft.Core.EventBus.RabbitMq;

namespace EasySoft.Core.EventBus.ExtensionMethods;

public static class ServiceCollectionExtension
{
    private const string UniqueIdentifierAddCapEventSubscriber = "752e99b7-2b62-4f7b-929a-eb1146d9c352";

    private const string UniqueIdentifierAddCapEventBus = "852de31f-db7e-4b99-abf7-d50782f8f3da";

    public static IServiceCollection AddCapEventSubscriber<TSubscriber>(
        this IServiceCollection services
    ) where TSubscriber : class, ICapSubscribe
    {
        if (services.HasRegistered(UniqueIdentifierAddCapEventSubscriber))
            return services;

        services.AddScoped<TSubscriber>();

        return services;
    }

    public static IServiceCollection AddCapEventBus(
        this IServiceCollection services
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