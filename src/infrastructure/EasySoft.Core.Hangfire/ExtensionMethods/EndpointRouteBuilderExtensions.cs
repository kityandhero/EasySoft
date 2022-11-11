using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Hangfire.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapHangfireConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapHangfireConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get hangfireConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/HangfireConfigFile").Join(" ")}, it only can access in development mode."
        );

        return endpoints.MapControllerRoute(
            "HangfireConfigFile",
            "{controller=HangfireConfigFile}/{action=Index}"
        ).WithDisplayName("HangfireConfigFile");
    }
}