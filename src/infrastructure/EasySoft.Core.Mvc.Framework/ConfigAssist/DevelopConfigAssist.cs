using EasySoft.Core.Mvc.Framework.ConfigCollection;
using EasySoft.Core.Mvc.Framework.Utils;
using Microsoft.Extensions.Configuration;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Mvc.Framework.ConfigAssist;

public static class DevelopConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(DevelopConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static DevelopConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(DevelopConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(DevelopConfig.Instance);
    }

    private static DevelopConfig GetConfig()
    {
        return DevelopConfig.Instance;
    }

    public static bool GetElasticSearchDataVersion()
    {
        var v = GetConfig().DevelopMode.Remove(" ").Trim();

        return v == "1";
    }
}