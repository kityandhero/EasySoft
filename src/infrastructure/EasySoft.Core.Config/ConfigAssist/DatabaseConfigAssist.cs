using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class DatabaseConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(DatabaseConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static DatabaseConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(filePath);

        Configuration = builder.Build();

        Configuration.Bind(DatabaseConfig.Instance);
    }

    public static void Init()
    {
    }

    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static DatabaseConfig GetConfig()
    {
        return DatabaseConfig.Instance;
    }

    public static string GetMainConnection()
    {
        var v = GetConfig().MainConnection;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置主库数据 MainConnection: {ConfigFile} -> MainConnection",
                GetConfigFileInfo()
            );

        return v;
    }

    public static string GetMirrorConnection()
    {
        var v = GetConfig().MirrorConnection;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置镜像数据库 MirrorConnection: {ConfigFile} -> MirrorConnection",
                GetConfigFileInfo()
            );

        return v;
    }

    public static string GetHistoryConnection()
    {
        var v = GetConfig().HistoryConnection;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置历史数据库 HistoryConnection: {ConfigFile} -> HistoryConnection",
                GetConfigFileInfo()
            );

        return v;
    }

    public static string GetHistoryErrorConnection()
    {
        var v = GetConfig().HistoryErrorConnection;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置历史迁移异常数据库 HistoryErrorConnection: {ConfigFile} -> HistoryErrorConnection",
                GetConfigFileInfo()
            );

        return v;
    }
}