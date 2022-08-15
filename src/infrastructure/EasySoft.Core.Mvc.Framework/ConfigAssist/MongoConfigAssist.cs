using EasySoft.Core.Mvc.Framework.ConfigCollection;
using EasySoft.Core.Mvc.Framework.Utils;
using Microsoft.Extensions.Configuration;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Mvc.Framework.ConfigAssist;

public static class MongoConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(MongoConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static MongoConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(MongoConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(MongoConfig.Instance);
    }

    private static MongoConfig GetConfig()
    {
        return MongoConfig.Instance;
    }

    public static string GetConnection()
    {
        var v = GetConfig().Connection;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置Mongo Connection: {ConfigFile} -> Connection"
            );
        }

        return v;
    }

    public static string GetDatabase()
    {
        var v = GetConfig().Database;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置Mongo Database: {ConfigFile} -> Database"
            );
        }

        return v.Remove(" ").Trim();
    }
}