namespace EasySoft.Core.Swagger.ExtensionMethods;

/// <summary>
/// EndpointConventionBuilderExtensions
/// </summary>
public static class EndpointConventionBuilderExtensions
{
    private const string Info = ", it only can access in development mode";

    internal static IEndpointRouteBuilder MapSwaggerConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapSwaggerConfigFile)}."
        );

        const string routeTemplate = "swaggerConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get swaggerConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=SwaggerConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "swaggerConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=SwaggerConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get swaggerConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }
}