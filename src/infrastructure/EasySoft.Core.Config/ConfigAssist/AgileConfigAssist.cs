using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// AgileConfigAssist
/// </summary>
public static class AgileConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(ConfigCollection.AgileConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static AgileConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(AgileConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(filePath);

        Configuration = builder.Build();

        Configuration.Bind(ConfigCollection.AgileConfig.Instance);
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

    private static ConfigCollection.AgileConfig GetConfig()
    {
        return ConfigCollection.AgileConfig.Instance;
    }

    /// <summary>
    /// 启用时必填: 后台管理中应用的应用ID
    /// </summary>
    /// <returns></returns>
    public static string GetAgileConfigAppId()
    {
        var v = GetConfig().AgileConfigAppId.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置 AgileConfigAppId: {ConfigFile} -> AgileConfigAppId",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// 启用时必填: 后台管理中应用的密钥
    /// </summary>
    /// <returns></returns>
    public static string GetAgileConfigSecret()
    {
        var v = GetConfig().AgileConfigSecret.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置 AgileConfigSecret: {ConfigFile} -> AgileConfigSecret",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// 启用时必填: 存在多个节点则使用逗号,分隔
    /// </summary>
    /// <returns></returns>
    public static List<string> GetAgileConfigNodeCollection()
    {
        var v = GetConfig().AgileConfigNodes.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置 AgileConfigAppId: {ConfigFile} -> AgileConfigAppId",
                GetConfigFileInfo()
            );

        return v.Split(",").ToListFilterNullOrWhiteSpace().ToList();
    }

    /// <summary>
    /// 可选: 方便在agile配置中心后台对当前客户端进行查阅与管理
    /// </summary>
    public static string GetAgileConfigName()
    {
        var v = GetConfig().AgileConfigName.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// 可选: 方便在agile配置中心后台对当前客户端进行查阅与管理
    /// </summary>
    public static string GetAgileConfigTag()
    {
        var v = GetConfig().AgileConfigTag.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// 可选: 通过此配置决定拉取哪个环境的配置信息；如果不配置，服务端会默认返回第一个环境的配置
    /// </summary>
    public static string GetAgileConfigEnv()
    {
        var v = GetConfig().AgileConfigEnv.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// 可选: 如设置了此目录则将拉取到的配置项cache文件存储到该目录，否则直接存储到站点根目录
    /// </summary>
    public static string GetAgileConfigCacheDirectory()
    {
        var v = GetConfig().AgileConfigCacheDirectory.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// 可选: 配置 client 发送 http 请求的时候的超时时间，默认100s
    /// </summary>
    public static int GetAgileConfigHttpTimeout()
    {
        var v = GetConfig().AgileConfigHttpTimeout.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "100" : v;

        if (!v.IsInt(out var value) || value <= 0)
            throw new ConfigErrorException(
                $"请配置 AgileConfigEnv: {ConfigFile} -> AgileConfigEnv,请设置数字 value > 0",
                GetConfigFileInfo()
            );

        return value;
    }
}