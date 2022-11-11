namespace EasySoft.Core.AccessWayTransmitter.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    public static ConfigureHostBuilder AddAccessWayTransmitter(
        this ConfigureHostBuilder builder
    )
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddAccessWayTransmitter();
        });

        return builder;
    }
}