using EasySoft.Core.ConsulConfigCenterClient.Assists;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.ConsulConfigCenterClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceConsulConfigCenter<T>(
        this WebApplicationBuilder builder,
        T channel,
        Action<IConfiguration>? action = null
    ) where T : IChannel
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceConsulConfigCenter)}<{typeof(T).Name}>."
        );

        if (ConsulFlagAssist.GetInitializeConfigWhetherComplete())
        {
            return builder;
        }

        builder.Host.AddAdvanceConsulConfigCenter(channel, action);

        ConsulFlagAssist.SetInitializeConfigComplete();

        return builder;
    }
}