using EasySoft.Core.Config.ConfigCollection;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

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
            typeof(MessageQueueConfig),
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

            itemPath.CreateFile(JsonConvertAssist.SerializeWithFormat(item.Create() ?? new { })
            );
        });
    }
}