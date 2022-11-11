namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceMediatR(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceMediatR)}."
        );

        builder.Host.AddAdvanceMediatR(assembly);

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceMediatR(
        this WebApplicationBuilder builder,
        IEnumerable<Assembly> assemblies
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceMediatR)}."
        );

        builder.Host.AddAdvanceMediatR(assemblies);

        return builder;
    }
}