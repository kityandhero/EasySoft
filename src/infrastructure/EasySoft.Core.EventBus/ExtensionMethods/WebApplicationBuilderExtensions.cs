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
        builder.Services.AddCapEventBus<TSubscriber>(option => { action?.Invoke(option); });

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(
                    $"Execute AddCapEventBus<{typeof(TSubscriber).Name}>."
                )
        );

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
        skyApmExtensions.AddCap();

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(
                    "Execute skyApmExtensions.AddCap()."
                )
        );

        return builder.AddCapEventBus<TSubscriber>(action);
    }
}