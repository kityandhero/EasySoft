using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
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
            true,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(MaintainConfig.Instance);
    }
    
    public static void Init()
    {
    }

    private static MaintainConfig GetConfig()
    {
        return MaintainConfig.Instance;
    }

    public static List<string> GetUrlPollingRequests()
    {
        var list = GetConfig().UrlPollingRequests.Remove(" ").Trim().Split(',').Where(o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        return list;
    }
}