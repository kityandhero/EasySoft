using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class AgileConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(ConfigCollection.AgileConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static AgileConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(AgileConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            true,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(ConfigCollection.AgileConfig.Instance);
    }

    public static void Init()
    {
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
        {
            throw new Exception(
                $"请配置 AgileConfigAppId: {ConfigFile} -> AgileConfigAppId"
            );
        }

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
        {
            throw new Exception(
                $"请配置 AgileConfigSecret: {ConfigFile} -> AgileConfigSecret"
            );
        }

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
        {
            throw new Exception(
                $"请配置 AgileConfigAppId: {ConfigFile} -> AgileConfigAppId"
            );
        }

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
        {
            throw new Exception(
                $"请配置 AgileConfigEnv: {ConfigFile} -> AgileConfigEnv,请设置数字 value > 0"
            );
        }

        return value;
    }
}