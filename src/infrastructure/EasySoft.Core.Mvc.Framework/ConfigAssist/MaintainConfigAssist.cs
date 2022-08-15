using EasySoft.Core.Mvc.Framework.ConfigCollection;
using EasySoft.Core.Mvc.Framework.Utils;
using Microsoft.Extensions.Configuration;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Mvc.Framework.ConfigAssist;

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
        var list = GetConfig().UrlPollingRequests.Remove(" ").Trim().Split(',')
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        return list;
    }
}