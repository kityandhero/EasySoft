using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.DevelopAuxiliary.ExtensionMethods;

public static class WebApplicationExtensions
{
    private const string UniqueIdentifierUseDevelopAuxiliary = "e84ad243-6571-4e74-aa77-02a3a8bb4ef1";

    public static WebApplication UseDevelopAuxiliary(
        this WebApplication application
    )
    {
        if (application.HasUsed(UniqueIdentifierUseDevelopAuxiliary))
            return application;

        if (!EnvironmentAssist.IsDevelopment()) return application;

        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseDevelopAuxiliary)}."
        );

        application.UseViewConfig(x => x.RenderPage());

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapActionMap(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapDatabaseConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapDevelopConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapElasticSearchConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapGeneralConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapMongoConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapRabbitMQConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapRedisConfigFile(); })
        );

        StartupConfigMessageAssist.AddConfig(
            $"UseAuxiliary: enabled."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/viewConfig ").Join(" ")} to get info such as appSetting, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/ActionMap").Join(" ")} to get all action info, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/DatabaseConfigFile").Join(" ")} to get databaseConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/DevelopConfigFile").Join(" ")} to get developConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/ElasticSearchConfigFile").Join(" ")} to get elasticSearchConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/GeneralConfigFile").Join(" ")} to get generalConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/MongoConfigFile").Join(" ")} to get mongoConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/RabbitMQConfigFile").Join(" ")} to get rabbitMQConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/NLogInlayConfig").Join(" ")} to get nLogInlayConfig template, display only in view development mode."
        );

        return application;
    }
}