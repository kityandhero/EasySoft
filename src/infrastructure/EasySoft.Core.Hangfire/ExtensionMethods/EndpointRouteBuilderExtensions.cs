using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Hangfire.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    private const string Info = ", it only can access in development mode";

    internal static IEndpointRouteBuilder MapHangfireConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapHangfireConfigFile)}."
        );

        const string routeTemplate = "hangfireConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get hangfireConfig template by access {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=HangfireConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "hangfireConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=HangfireConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get hangfireConfig template by access {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }
}