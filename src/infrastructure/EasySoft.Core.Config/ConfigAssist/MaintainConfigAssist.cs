using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// MaintainConfigAssist
/// </summary>
public static class MaintainConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(MaintainConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static MaintainConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(MaintainConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(MaintainConfig.Instance);
    }

    /// <summary>
    /// Init
    /// </summary>
    public static void Init()
    {
    }

    /// <summary>
    /// 获取配置文件路径
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFilePath()
    {
        return FilePath;
    }

    /// <summary>
    /// 获取配置文件内容
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetConfigFileContent()
    {
        var content = await GetConfigFilePath().ReadFile();

        return string.IsNullOrWhiteSpace(content) ? content : JsonConvertAssist.FormatText(content);
    }

    /// <summary>
    /// 获取配置文件信息
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static MaintainConfig GetConfig()
    {
        return MaintainConfig.Instance;
    }

    /// <summary>
    /// GetUrlPollingRequests
    /// </summary>
    /// <returns></returns>
    public static List<string> GetUrlPollingRequests()
    {
        var list = GetConfig().UrlPollingRequests.Remove(" ").Trim().Split(',')
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        return list;
    }
}