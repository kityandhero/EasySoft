using DotNetCore.CAP;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using SkyApm.Diagnostics.CAP;
using SkyApm.Utilities.DependencyInjection;

namespace EasySoft.Core.EventBus.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注册CAP组件(实现事件总线及最终一致性（分布式事务）的一个开源的组件)
    /// </summary>
    public static WebApplicationBuilder AddCapEventBus<TSubscriber>(
        this WebApplicationBuilder builder,
        Action<CapOptions>? action = null
    ) where TSubscriber : class, ICapSubscribe
    {
        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(
                    $"Execute {nameof(AddCapEventBus)}<{typeof(TSubscriber).Name}>()."
                )
        );

        builder.Services.AddCapEventBus<TSubscriber>(option => { action?.Invoke(option); });

        return builder;
    }

    /// <summary>
    /// 注册CAP组件(实现事件总线及最终一致性（分布式事务）的一个开源的组件)
    /// </summary>
    public static WebApplicationBuilder AddCapEventBus<TSubscriber>(
        this WebApplicationBuilder builder,
        SkyApmExtensions skyApmExtensions,
        Action<CapOptions>? action = null
    ) where TSubscriber : class, ICapSubscribe
    {
        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(
                    "Execute skyApmExtensions.AddCap()."
                )
        );

        skyApmExtensions.AddCap();

        return builder.AddCapEventBus<TSubscriber>(action);
    }
}