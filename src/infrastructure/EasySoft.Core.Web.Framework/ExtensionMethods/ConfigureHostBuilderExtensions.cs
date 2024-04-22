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
        builder.ConfigureContainer<ContainerBuilder>(
            containerBuilder =>
            {
                containerBuilder.RegisterInstance(new ApplicationChannel().SetChannel(channel))
                    .As<IApplicationChannel>()
                    .SingleInstance();
            }
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"ApplicationChannel name is {channel.Name}, identification is {channel.Value}."
        );

        return builder;
    }
}