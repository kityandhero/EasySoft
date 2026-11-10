using IChannel = EasySoft.UtilityTools.Standard.Interfaces.IChannel;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceDefaultApplicationChannel(
        this ConfigureHostBuilder builder
    )
    {
        builder.AddAdvanceApplicationChannel(
            ApplicationChannel.DefaultChannel
        );

        return builder;
    }

    internal static ConfigureHostBuilder AddAdvanceApplicationChannel(
        this ConfigureHostBuilder builder,
        IChannel channel
    )
    {
        var applicationChannel = new ApplicationChannel().SetChannel(channel);

        builder.ConfigureServices(services => { services.AddSingleton<IApplicationChannel>(applicationChannel); });

        StartupDescriptionMessageAssist.AddPrompt(
            $"ApplicationChannel name is {channel.Name}, identification is {channel.Value}."
        );

        return builder;
    }
}