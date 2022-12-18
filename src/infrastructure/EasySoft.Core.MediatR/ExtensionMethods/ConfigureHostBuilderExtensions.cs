namespace EasySoft.Core.MediatR.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceMediatR(
        this ConfigureHostBuilder builder
    )
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddAdvanceMediator(MediatRConfigure.GetAssemblies());

            StartupDescriptionMessageAssist.AddHint(
                $"{typeof(MediatRConfigure).FullName}.{nameof(MediatRConfigure.GetAssemblies)} contain {(!MediatRConfigure.GetAssemblies().Any() ? "none" : MediatRConfigure.GetAssemblies().Select(o => o.GetName().Name).Join(","))}."
            );
        });

        return builder;
    }
}