using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class EnvironmentConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(EnvironmentConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static EnvironmentConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder();

        builder.AddJsonFile(
            filePath,
            true,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(EnvironmentConfig.Instance);
    }

    public static void Init()
    {
    }

    private static EnvironmentConfig GetConfig()
    {
        return EnvironmentConfig.Instance;
    }

    public static string GetCustomEnv()
    {
        var v = GetConfig().CustomEnv.Remove(" ").Trim().ToLower();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }
}