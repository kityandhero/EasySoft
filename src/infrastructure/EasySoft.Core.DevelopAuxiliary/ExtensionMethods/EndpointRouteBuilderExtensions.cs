using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.DevelopAuxiliary.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapActionMap(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "ActionMap",
            "{controller=ActionMap}/{action=Index}"
        ).WithDisplayName("ActionMap");
    }

    internal static IEndpointConventionBuilder MapDatabaseConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "DatabaseConfigFile",
            "{controller=DatabaseConfigFile}/{action=Index}"
        ).WithDisplayName("DatabaseConfigFile");
    }

    internal static IEndpointConventionBuilder MapDevelopConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "DevelopConfigFile",
            "{controller=DevelopConfigFile}/{action=Index}"
        ).WithDisplayName("DevelopConfigFile");
    }

    internal static IEndpointConventionBuilder MapElasticSearchConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "ElasticSearchConfigFile",
            "{controller=ElasticSearchConfigFile}/{action=Index}"
        ).WithDisplayName("ElasticSearchConfigFile");
    }

    internal static IEndpointConventionBuilder MapGeneralConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "GeneralConfigFile",
            "{controller=GeneralConfigFile}/{action=Index}"
        ).WithDisplayName("GeneralConfigFile");
    }

    internal static IEndpointConventionBuilder MapMongoConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "MongoConfigFile",
            "{controller=MongoConfigFile}/{action=Index}"
        ).WithDisplayName("MongoConfigFile");
    }

    internal static IEndpointConventionBuilder MapRabbitMQConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "RabbitMQConfigFile",
            "{controller=RabbitMQConfigFile}/{action=Index}"
        ).WithDisplayName("RabbitMQConfigFile");
    }

    internal static IEndpointConventionBuilder MapRedisConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "RedisConfigFile",
            "{controller=RedisConfigFile}/{action=Index}"
        ).WithDisplayName("RedisConfigFile");
    }

    internal static IEndpointConventionBuilder MapNLogInlayConfig(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "NLogInlayConfig",
            "{controller=NLogInlayConfig}/{action=Index}"
        ).WithDisplayName("NLogInlayConfig");
    }
}