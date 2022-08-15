using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class HangfireConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(HangfireConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static HangfireConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(HangfireConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(HangfireConfig.Instance);
    }
    
    public static void Init()
    {
    }

    private static HangfireConfig GetConfig()
    {
        return HangfireConfig.Instance;
    }

    public static bool GetEnable()
    {
        var v = GetConfig().Enable;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置Swagger Enable: {ConfigFile} -> Enable,请设置 0/1"
            );
        }

        return v.ToInt() == 1;
    }

    public static string GetStorage()
    {
        var v = GetConfig().Storage;

        v = v.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置Swagger Enable: {ConfigFile} -> Storage"
            );
        }

        return v;
    }
}