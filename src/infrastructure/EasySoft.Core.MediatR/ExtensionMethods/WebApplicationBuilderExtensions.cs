namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceMediatR = "d0fd46db-ea46-461a-bd0b-2871af2d2fbf";

    public static WebApplicationBuilder AddAdvanceMediatR(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceMediatR))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceMediatR)}."
        );

        builder.Host.AddAdvanceMediatR();

        return builder;
    }
}