using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Configures;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class HostApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceMediatR = "d0fd46db-ea46-461a-bd0b-2871af2d2fbf";

    public static IHostApplicationBuilder AddAdvanceMediatR(
        this IHostApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceMediatR))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceMediatR)}."
        );

        builder.Services.AddAdvanceMediatR();

        return builder;
    }
}