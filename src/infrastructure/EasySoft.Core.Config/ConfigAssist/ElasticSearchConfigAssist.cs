using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class ElasticSearchConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(ElasticSearchConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static ElasticSearchConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(ElasticSearchConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            true,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(ElasticSearchConfig.Instance);
    }
    
    public static void Init()
    {
    }

    private static ElasticSearchConfig GetConfig()
    {
        return ElasticSearchConfig.Instance;
    }

    public static int GetElasticSearchDataVersion()
    {
        var v = GetConfig().ElasticSearchDataVersion;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception("缺少ElasticSearchDataVersion配置（ElasticSearchDataVersion）"
            );
        }

        return v.ToInt();
    }
}