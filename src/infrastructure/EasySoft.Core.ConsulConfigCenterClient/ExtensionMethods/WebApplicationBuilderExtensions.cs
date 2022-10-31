using EasySoft.Core.ConsulConfigCenterClient.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.ConsulConfigCenterClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceConsulConfigCenter<T>(
        this WebApplicationBuilder builder,
        T applicationChannel,
        Action<IConfiguration>? action = null
    ) where T : IApplicationChannel
    {
        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(
                    $"Execute {nameof(AddAdvanceConsulConfigCenter)}<{typeof(T).Name}>()."
                )
        );

        if (ConsulFlagAssist.GetInitializeConfigWhetherComplete()) return builder;

        builder.Host.AddAdvanceConsulConfigCenter<T>(applicationChannel, action);

        ConsulFlagAssist.SetInitializeConfigComplete();

        return builder;
    }
}