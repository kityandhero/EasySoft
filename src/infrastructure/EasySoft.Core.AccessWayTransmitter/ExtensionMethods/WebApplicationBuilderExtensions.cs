using Autofac;
using DotNetCore.CAP;
using DotNetCore.CAP.Filter;
using EasySoft.Core.AccessWayTransmitter.Assists;
using EasySoft.Core.AccessWayTransmitter.Producers;
using EasySoft.Core.Cap.Transport.ExtensionMethods;
using EasySoft.Core.Cap.Persistent;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.AccessWayTransmitter.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="persistentType"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAccessWayTransmitter(
        this WebApplicationBuilder builder,
        PersistentType persistentType
    )
    {
        if (InitialAssist.InitialComplete)
        {
            return builder;
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<AccessWayProducer>().As<IAccessWayProducer>().SingleInstance();
        });

        builder.AddAdvanceCapRabbitMQ(persistentType);

        InitialAssist.InitialComplete = true;

        return builder;
    }

    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="action"></param>
    /// <param name="persistentType"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAccessWayTransmitter(
        this WebApplicationBuilder builder,
        Action<RabbitMQOptions> action,
        PersistentType persistentType
    )
    {
        if (InitialAssist.InitialComplete)
        {
            return builder;
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<AccessWayProducer>().As<IAccessWayProducer>().SingleInstance();
        });

        builder.AddAdvanceCapRabbitMQ(action, persistentType);

        InitialAssist.InitialComplete = true;

        return builder;
    }

    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="persistentType"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAccessWayTransmitter<T>(
        this WebApplicationBuilder builder,
        PersistentType persistentType
    ) where T : SubscribeFilter
    {
        if (InitialAssist.InitialComplete)
        {
            return builder;
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<AccessWayProducer>().As<IAccessWayProducer>().SingleInstance();
        });

        builder.AddAdvanceCapRabbitMQ<T>(persistentType);

        InitialAssist.InitialComplete = true;

        return builder;
    }

    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="action"></param>
    /// <param name="persistentType"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAccessWayTransmitter<T>(
        this WebApplicationBuilder builder,
        Action<RabbitMQOptions> action,
        PersistentType persistentType
    ) where T : SubscribeFilter
    {
        if (InitialAssist.InitialComplete)
        {
            return builder;
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<AccessWayProducer>().As<IAccessWayProducer>().SingleInstance();
        });

        builder.AddAdvanceCapRabbitMQ<T>(action, persistentType);

        InitialAssist.InitialComplete = true;

        return builder;
    }
}