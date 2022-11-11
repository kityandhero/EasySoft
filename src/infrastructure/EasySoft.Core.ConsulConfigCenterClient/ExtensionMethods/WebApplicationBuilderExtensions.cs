using EasySoft.Core.ConsulConfigCenterClient.Assists;

namespace EasySoft.Core.ConsulConfigCenterClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceConsulConfigCenter<T>(
        this WebApplicationBuilder builder,
        T applicationChannel,
        Action<IConfiguration>? action = null
    ) where T : IApplicationChannel
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceConsulConfigCenter)}<{typeof(T).Name}>."
        );

        if (ConsulFlagAssist.GetInitializeConfigWhetherComplete()) return builder;

        builder.Host.AddAdvanceConsulConfigCenter<T>(applicationChannel, action);

        ConsulFlagAssist.SetInitializeConfigComplete();

        return builder;
    }
}