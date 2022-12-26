using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// PayCallbackConfigAssist
/// </summary>
public static class PayCallbackConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(PayCallbackConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static PayCallbackConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(PayCallbackConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(PayCallbackConfig.Instance);
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

    private static PayCallbackConfig GetConfig()
    {
        return PayCallbackConfig.Instance;
    }

    /// <summary>
    /// GetCallbackHost
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetCallbackHost()
    {
        var v = GetConfig().CallbackHost;

        v = v.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v)) throw new Exception("缺少支付回调配置（PayCallbackHost）");

        return v;
    }
}