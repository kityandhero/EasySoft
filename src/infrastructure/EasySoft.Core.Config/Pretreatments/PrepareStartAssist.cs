﻿using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Config.Pretreatments;

public static class PrepareStartAssist
{
    public static void ToWork()
    {
        CheckConfigures();
    }

    private static void CheckConfigures()
    {
        var directory = AppContextAssist.GetBaseDirectory();

        var configureFolderPath = $"{directory}/configures/";

        configureFolderPath.CreateDirectory();

        var configureSimpleFolderPath = $"{directory}/configures/simples/";

        configureSimpleFolderPath.DeleteDirectory();

        configureSimpleFolderPath.CreateDirectory();

        var configureFileNameList = new List<Type>
        {
            typeof(SwaggerConfig),
            typeof(GeneralConfig),
            typeof(HangfireConfig),
            typeof(BusinessConfig),
            typeof(DatabaseConfig),
            typeof(DevelopConfig),
            typeof(ElasticSearchConfig),
            typeof(JobConfig),
            typeof(LogConfig),
            typeof(MaintainConfig),
            typeof(RabbitMQConfig),
            typeof(MongoConfig),
            typeof(PayCallbackConfig),
            typeof(RedisConfig),
            typeof(ServiceConfig),
        };

        configureFileNameList.ForEach(item =>
        {
            var itemPath = $"{directory}\\configures\\{item.Name.ToLowerFirst()}.json";

            itemPath.CreateFile(JsonConvertAssist.SerializeAndKeyToLower(new { }));
        });

        configureFileNameList.ForEach(item =>
        {
            var itemPath = $"{directory}\\configures\\simples\\{item.Name.ToLowerFirst()}.json";

            if (item.FullName != typeof(LogConfig).FullName)
            {
                itemPath.CreateFile(
                    JsonConvertAssist.SerializeWithFormat(item.Create() ?? new { })
                );
            }
            else
            {
                itemPath.CreateFile(Tools.GetNlogDefaultConfig());
            }
        });
        
        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                "All sample config file has generated in ./configures/simple folder."
                )
        );
    }
}