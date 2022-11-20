namespace EasySoft.Core.EnvironmentAuxiliary.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    private const string Info = ", it only can access in development mode";

    internal static IEndpointRouteBuilder MapAssemblyMap(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapActionMap)}."
        );

        const string routeTemplate = "environmentAuxiliary/assemblyMap";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get action map by access {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=EnvironmentAuxiliary}/{action=AssemblyMap}"
        );

        return endpoints;
    }

    internal static IEndpointRouteBuilder MapActionMap(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapActionMap)}."
        );

        const string routeTemplate = "environmentAuxiliary/actionMap";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get action map by access {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=EnvironmentAuxiliary}/{action=ActionMap}"
        );

        return endpoints;
    }
}