using EasySoft.Core.Config.ConfigCollection;
using EasySoft.UtilityTools.Assists;
using IOExtensions = EasySoft.UtilityTools.ExtensionMethods.IOExtensions;
using ReflectionExtensions = EasySoft.UtilityTools.ExtensionMethods.ReflectionExtensions;
using StringExtensions = EasySoft.UtilityTools.ExtensionMethods.StringExtensions;

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

        IOExtensions.CreateDirectory(configureFolderPath);

        var configureSimpleFolderPath = $"{directory}/configures/simples/";

        IOExtensions.DeleteDirectory(configureSimpleFolderPath);

        IOExtensions.CreateDirectory(configureSimpleFolderPath);

        var configureFileNameList = new List<Type>
        {
            typeof(SwaggerConfig),
            typeof(HangfireConfig),
            typeof(BusinessConfig),
            typeof(DatabaseConfig),
            typeof(DevelopConfig),
            typeof(ElasticSearchConfig),
            typeof(JobConfig),
            typeof(LogConfig),
            typeof(MaintainConfig),
            typeof(MessageQueueConfig),
            typeof(MongoConfig),
            typeof(PayCallbackConfig),
            typeof(RedisConfig),
            typeof(ServiceConfig),
        };

        configureFileNameList.ForEach(item =>
        {
            var itemPath = $"{directory}\\configures\\{StringExtensions.ToLowerFirst(item.Name)}.json";

            IOExtensions.CreateFile(itemPath, JsonConvertAssist.SerializeAndKeyToLower(new { }));
        });

        configureFileNameList.ForEach(item =>
        {
            var itemPath = $"{directory}\\configures\\simples\\{StringExtensions.ToLowerFirst(item.Name)}.json";

            IOExtensions.CreateFile(itemPath,
                JsonConvertAssist.SerializeWithFormat(ReflectionExtensions.Create(item) ?? new { }));
        });
    }
}