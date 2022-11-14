using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Config.ConfigAssist;

public static class ElasticSearchConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(ElasticSearchConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static ElasticSearchConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(ElasticSearchConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(ElasticSearchConfig.Instance);
    }

    public static void Init()
    {
    }

    public static string GetConfigFilePath()
    {
        return FilePath;
    }

    public static async Task<string> GetConfigFileContent()
    {
        var content = await FilePath.ReadFile();

        return string.IsNullOrWhiteSpace(content) ? content : JsonConvertAssist.FormatText(content);
    }

    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
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
            throw new Exception("缺少ElasticSearchDataVersion配置（ElasticSearchDataVersion）"
            );

        return v.ToInt();
    }
}