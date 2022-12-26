using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// SuperConfigAssist
/// </summary>
public static class SuperConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(SuperConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static SuperConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(SuperConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(filePath);

        Configuration = builder.Build();

        Configuration.Bind(SuperConfig.Instance);
    }

    /// <summary>
    /// Init
    /// </summary>
    public static void Init()
    {
    }

    /// <summary>
    /// 获取配置文件信息
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static SuperConfig GetConfig()
    {
        return SuperConfig.Instance;
    }

    /// <summary>
    /// 获取超级密码
    /// </summary>
    public static string GetPassword()
    {
        var v = GetConfig().Password;

        v = v.Remove(" ").Trim();

        return string.IsNullOrWhiteSpace(v) ? "" : v;
    }
}