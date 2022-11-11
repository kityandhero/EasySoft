using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.EasyCaching.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapRedisConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapRedisConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get redisConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/RedisConfigFile").Join(" ")}, it only can access in development mode."
        );

        return endpoints.MapControllerRoute(
            "RedisConfigFile",
            "{controller=RedisConfigFile}/{action=Index}"
        ).WithDisplayName("RedisConfigFile");
    }
}