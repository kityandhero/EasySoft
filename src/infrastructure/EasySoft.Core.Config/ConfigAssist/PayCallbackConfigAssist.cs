using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Config.ConfigAssist;

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

    private static PayCallbackConfig GetConfig()
    {
        return PayCallbackConfig.Instance;
    }

    public static string GetCallbackHost()
    {
        var v = GetConfig().CallbackHost;

        v = v.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v)) throw new Exception("缺少支付回调配置（PayCallbackHost）");

        return v;
    }
}