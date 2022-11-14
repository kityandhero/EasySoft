namespace EasySoft.Core.EasyCaching.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    private const string Info = ", it only can access in development mode";

    internal static IEndpointRouteBuilder MapRedisConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapRedisConfigFile)}."
        );

        const string routeTemplate = "redisConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get redisConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=RedisConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "redisConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=RedisConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get redisConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }
}