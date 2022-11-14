using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Config.ConfigAssist;

public static class MongoConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(MongoConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static MongoConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(MongoConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(MongoConfig.Instance);
    }

    public static void Init()
    {
    }

    public static string GetConfigFilePath()
    {
        return FilePath;
    }

    public static async Task<string> GetConfigFileContent()
    {
        var content = await FilePath.ReadFile();

        return string.IsNullOrWhiteSpace(content) ? content : JsonConvertAssist.FormatText(content);
    }

    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static MongoConfig GetConfig()
    {
        return MongoConfig.Instance;
    }

    public static string GetConnection()
    {
        var v = GetConfig().Connection;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置Mongo Connection: {ConfigFile} -> Connection",
                GetConfigFileInfo()
            );

        return v;
    }

    public static string GetDatabase()
    {
        var v = GetConfig().Database;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置Mongo Database: {ConfigFile} -> Database",
                GetConfigFileInfo()
            );

        return v.Remove(" ").Trim();
    }
}