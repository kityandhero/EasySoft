namespace EasySoft.Core.Gateway.Ocelot.ExtensionMethods;

/// <summary>
/// EndpointConventionBuilderExtensions
/// </summary>
public static class EndpointConventionBuilderExtensions
{
    private const string Info = ", it only can access in development mode";

    internal static IEndpointRouteBuilder MapOcelotConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapOcelotConfigFile)}."
        );

        const string routeTemplate = "ocelotConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get ocelotConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=OcelotConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "ocelotConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=OcelotConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get ocelotConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }
}