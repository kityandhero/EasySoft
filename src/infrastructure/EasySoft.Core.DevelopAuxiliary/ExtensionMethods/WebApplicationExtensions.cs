using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.DevelopAuxiliary.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseDevelopAuxiliary(
        this WebApplication application
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseDevelopAuxiliary)}()."
        );

        if (FlagAssist.GetDevelopAuxiliarySwitch()) return application;

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
                .SetAction(endpoints => { endpoints.MapHangfireConfigFile(); })
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

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapSwaggerConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapNLogInlayConfig(); })
        );

        FlagAssist.SetActionMapSwitchOpen();

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
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/HangfireConfigFile").Join(" ")} to get hangfireConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/MongoConfigFile").Join(" ")} to get mongoConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/RabbitMQConfigFile").Join(" ")} to get rabbitMQConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/RedisConfigFile").Join(" ")} to get redisConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/SwaggerConfigFile").Join(" ")} to get swaggerConfig template, display only in view development mode."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/NLogInlayConfig").Join(" ")} to get nLogInlayConfig template, display only in view development mode."
        );

        return application;
    }
}