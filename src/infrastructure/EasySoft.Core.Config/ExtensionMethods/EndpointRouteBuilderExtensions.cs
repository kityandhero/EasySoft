using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Config.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    private const string Info = ", it only can access in development mode";

    internal static IEndpointRouteBuilder MapDatabaseConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapDatabaseConfigFile)}."
        );

        const string routeTemplate = "databaseConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get databaseConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            "DatabaseConfigFile",
            "{controller=DatabaseConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "databaseConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=DatabaseConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get databaseConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }

    internal static IEndpointRouteBuilder MapDevelopConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapDevelopConfigFile)}."
        );

        const string routeTemplate = "developConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get developConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=DevelopConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "developConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=DevelopConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get developConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }

    internal static IEndpointRouteBuilder MapElasticSearchConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapElasticSearchConfigFile)}."
        );

        const string routeTemplate = "elasticSearchConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get elasticSearchConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=ElasticSearchConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "elasticSearchConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=ElasticSearchConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get elasticSearchConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }

    internal static IEndpointRouteBuilder MapGeneralConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapGeneralConfigFile)}."
        );

        const string routeTemplate = "generalConfigAuxiliary/getTemplate";

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=GeneralConfigAuxiliary}/{action=GetTemplate}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get generalConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        const string routeCurrent = "generalConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=GeneralConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get generalConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }

    internal static IEndpointRouteBuilder MapMongoConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapMongoConfigFile)}."
        );

        const string routeTemplate = "mongoConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get mongoConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=MongoConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "elasticSearchConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=MongoConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get mongoConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }

    internal static IEndpointRouteBuilder MapRabbitMQConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapRabbitMQConfigFile)}."
        );

        const string routeTemplate = "rabbitMQConfigAuxiliary/getTemplate";

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get rabbitMQConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeTemplate}").Join(" ")}{Info}."
        );

        endpoints.MapControllerRoute(
            routeTemplate,
            "{controller=RabbitMQConfigAuxiliary}/{action=GetTemplate}"
        );

        const string routeCurrent = "rabbitMQConfigAuxiliary/getCurrent";

        endpoints.MapControllerRoute(
            routeCurrent,
            "{controller=RabbitMQConfigAuxiliary}/{action=GetCurrent}"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get rabbitMQConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/{routeCurrent}").Join(" ")}{Info}."
        );

        return endpoints;
    }
}