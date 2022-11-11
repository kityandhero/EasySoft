using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.NLog.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapNLogInlayConfig(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapNLogInlayConfig)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get nLogInlayConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/NLogInlayConfig").Join(" ")}, it only can access in development mode."
        );

        return endpoints.MapControllerRoute(
            "NLogInlayConfig",
            "{controller=NLogInlayConfig}/{action=Index}"
        ).WithDisplayName("NLogInlayConfig");
    }
}