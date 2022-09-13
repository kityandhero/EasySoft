using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Auxiliary.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAuxiliary(
        this WebApplication application
    )
    {
        if (FlagAssist.GetAuxiliarySwitch())
        {
            return application;
        }

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapActionMap")
                .SetAction(endpoints => { endpoints.MapActionMap(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapDatabaseConfigFile")
                .SetAction(endpoints => { endpoints.MapDatabaseConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapDevelopConfigFile")
                .SetAction(endpoints => { endpoints.MapDevelopConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapElasticSearchConfigFile")
                .SetAction(endpoints => { endpoints.MapElasticSearchConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapGeneralConfigFile")
                .SetAction(endpoints => { endpoints.MapGeneralConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapHangfireConfigFile")
                .SetAction(endpoints => { endpoints.MapHangfireConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapMongoConfigFile")
                .SetAction(endpoints => { endpoints.MapMongoConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapRabbitMQConfigFile")
                .SetAction(endpoints => { endpoints.MapRabbitMQConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapRedisConfigFile")
                .SetAction(endpoints => { endpoints.MapRedisConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapSwaggerConfigFile")
                .SetAction(endpoints => { endpoints.MapSwaggerConfigFile(); })
        );
        
             ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
                    new ExtraAction<IEndpointRouteBuilder>()
                        .SetName("NLogInlayConfig")
                        .SetAction(endpoints => { endpoints.MapNLogInlayConfig(); })
                );

        FlagAssist.SetActionMapSwitchOpen();

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"UseAuxiliary: enabled."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/ActionMap").Join(" ")} to get all action info."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/DatabaseConfigFile").Join(" ")} to get databaseConfig template."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/DevelopConfigFile").Join(" ")} to get developConfig template."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/ElasticSearchConfigFile").Join(" ")} to get elasticSearchConfig template."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/GeneralConfigFile").Join(" ")} to get generalConfig template."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/HangfireConfigFile").Join(" ")} to get hangfireConfig template."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/MongoConfigFile").Join(" ")} to get mongoConfig template."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/RabbitMQConfigFile").Join(" ")} to get rabbitMQConfig template."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/RedisConfigFile").Join(" ")} to get redisConfig template."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/SwaggerConfigFile").Join(" ")} to get swaggerConfig template."
                )
        );
        
           StartupDescriptionMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/NLogInlayConfig").Join(" ")} to get nLogInlayConfig template."
                        )
                );

        return application;
    }
}