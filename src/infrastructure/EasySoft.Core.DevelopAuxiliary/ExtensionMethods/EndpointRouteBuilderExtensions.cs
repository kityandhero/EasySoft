namespace EasySoft.Core.DevelopAuxiliary.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    private const string Info = ", it only can access in development mode";

    internal static IEndpointConventionBuilder MapActionMap(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapActionMap)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get action map by access {FlagAssist.StartupUrls.Select(o => $"{o}/ActionMap").Join(" ")}{Info}."
        );

        return endpoints.MapControllerRoute(
            "ActionMap",
            "{controller=ActionMap}/{action=Index}"
        ).WithDisplayName("ActionMap");
    }

    internal static IEndpointConventionBuilder MapDatabaseConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapDatabaseConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get databaseConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/DatabaseConfigFile").Join(" ")}{Info}."
        );

        return endpoints.MapControllerRoute(
            "DatabaseConfigFile",
            "{controller=DatabaseConfigFile}/{action=Index}"
        ).WithDisplayName("DatabaseConfigFile");
    }

    internal static IEndpointConventionBuilder MapDevelopConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapDevelopConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get developConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/DevelopConfigFile").Join(" ")}{Info}."
        );

        return endpoints.MapControllerRoute(
            "DevelopConfigFile",
            "{controller=DevelopConfigFile}/{action=Index}"
        ).WithDisplayName("DevelopConfigFile");
    }

    internal static IEndpointConventionBuilder MapElasticSearchConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapElasticSearchConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get elasticSearchConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/ElasticSearchConfigFile").Join(" ")}{Info}."
        );

        return endpoints.MapControllerRoute(
            "ElasticSearchConfigFile",
            "{controller=ElasticSearchConfigFile}/{action=Index}"
        ).WithDisplayName("ElasticSearchConfigFile");
    }

    internal static IEndpointConventionBuilder MapGeneralConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapGeneralConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get generalConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/GeneralConfigFile").Join(" ")}{Info}."
        );

        return endpoints.MapControllerRoute(
            "GeneralConfigFile",
            "{controller=GeneralConfigFile}/{action=Index}"
        ).WithDisplayName("GeneralConfigFile");
    }

    internal static IEndpointConventionBuilder MapMongoConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapMongoConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get mongoConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/MongoConfigFile").Join(" ")}{Info}."
        );

        return endpoints.MapControllerRoute(
            "MongoConfigFile",
            "{controller=MongoConfigFile}/{action=Index}"
        ).WithDisplayName("MongoConfigFile");
    }

    internal static IEndpointConventionBuilder MapRabbitMQConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapRabbitMQConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get rabbitMQConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/RabbitMQConfigFile").Join(" ")}{Info}."
        );

        return endpoints.MapControllerRoute(
            "RabbitMQConfigFile",
            "{controller=RabbitMQConfigFile}/{action=Index}"
        ).WithDisplayName("RabbitMQConfigFile");
    }
}