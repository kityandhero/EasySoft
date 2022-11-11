using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.Hangfire.ConfigCollection;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Hangfire.ConfigAssist;

public static class HangfireConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(HangfireConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static HangfireConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(HangfireConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(filePath);

        Configuration = builder.Build();

        Configuration.Bind(HangfireConfig.Instance);
    }

    public static void Init()
    {
    }

    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static HangfireConfig GetConfig()
    {
        return HangfireConfig.Instance;
    }

    public static bool GetSwitch()
    {
        var v = GetConfig().Switch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
            throw new ConfigErrorException(
                $"请配置Hangfire Switch: {ConfigFile} -> Enable,请设置 0/1",
                GetConfigFileInfo()
            );

        return v.ToInt() == 1;
    }

    public static string GetStorageType()
    {
        var v = GetConfig().StorageType;

        v = v.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置Hangfire StorageType: {ConfigFile} -> StorageType",
                GetConfigFileInfo()
            );

        return v;
    }

    public static string GetStorageConnection()
    {
        var v = GetConfig().StorageConnection;

        v = v.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置Hangfire StorageConnection: {ConfigFile} -> StorageConnection",
                GetConfigFileInfo()
            );

        return v;
    }
}