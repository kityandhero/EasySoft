using DotNetCore.CAP;
using DotNetCore.CAP.Filter;
using EasySoft.Core.Cap.Persistent;
using EasySoft.Core.Cap.Transport.ExtensionMethods.RabbitMQ;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Cap.Transport.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceCapRabbitMQ(
        this WebApplicationBuilder builder,
        PersistentType persistentType
    )
    {
        builder.Services.AddAdvanceCapRabbitMQ(persistentType);

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceCapRabbitMQ(
        this WebApplicationBuilder builder,
        Action<RabbitMQOptions> action,
        PersistentType persistentType
    )
    {
        builder.Services.AddAdvanceCapRabbitMQ(action, persistentType);

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceCapRabbitMQ<T>(
        this WebApplicationBuilder builder,
        PersistentType persistentType
    ) where T : SubscribeFilter
    {
        builder.Services.AddAdvanceCapRabbitMQ<T>(persistentType);

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceCapRabbitMQ<T>(
        this WebApplicationBuilder builder,
        Action<RabbitMQOptions> action,
        PersistentType persistentType
    ) where T : SubscribeFilter
    {
        builder.Services.AddAdvanceCapRabbitMQ<T>(action, persistentType);

        return builder;
    }
}