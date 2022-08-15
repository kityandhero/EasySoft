using Framework.ConfigCollection;
using Framework.Utils;
using Microsoft.Extensions.Configuration;
using UtilityTools.ExtensionMethods;

namespace Framework.ConfigAssist;

public static class LogConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(LogConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static LogConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(LogConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(LogConfig.Instance);
    }

    private static LogConfig GetConfig()
    {
        return LogConfig.Instance;
    }

    public static bool GetElasticSearchDataVersion()
    {
        var v = GetConfig().LogWeChatSessionToRemote;

        v = v.Remove(" ").Trim();

        return v == "1";
    }
}