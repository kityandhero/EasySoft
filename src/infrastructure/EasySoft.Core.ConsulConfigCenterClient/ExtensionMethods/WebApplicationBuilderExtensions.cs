using EasySoft.Core.ConsulConfigCenterClient.Assists;
using EasySoft.UtilityTools.Core.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.ConsulConfigCenterClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceConsulConfigCenter<T>(
        this WebApplicationBuilder builder,
        T applicationChannel,
        Action<IConfiguration>? action = null
    ) where T : IApplicationChannel
    {
        if (ConsulFlagAssist.GetInitializeConfigWhetherComplete())
        {
            return builder;
        }

        builder.Host.AddAdvanceConsulConfigCenter<T>(applicationChannel, action);

        ConsulFlagAssist.SetInitializeConfigComplete();

        return builder;
    }
}