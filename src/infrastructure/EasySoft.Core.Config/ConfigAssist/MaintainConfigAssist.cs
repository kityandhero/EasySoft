using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class MaintainConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(MaintainConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static MaintainConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(MaintainConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(MaintainConfig.Instance);
    }

    private static MaintainConfig GetConfig()
    {
        return MaintainConfig.Instance;
    }

    public static List<string> GetUrlPollingRequests()
    {
        var list = Enumerable.Where<string>(GetConfig().UrlPollingRequests.Remove(" ").Trim().Split(','), o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        return list;
    }
}