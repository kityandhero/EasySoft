namespace EasySoft.Core.Config.ConfigCollection;

public class AgileConfig
{
    public static readonly AgileConfig Instance = new();

    /// <summary>
    /// 必填: 后台管理中应用的应用ID
    /// </summary>
    public string AgileConfigAppId { get; set; }

    /// <summary>
    /// 必填: 后台管理中应用的密钥
    /// </summary>
    public string AgileConfigSecret { get; set; }

    /// <summary>
    /// 必填: 存在多个节点则使用逗号,分隔
    /// </summary>
    public string AgileConfigNodes { get; set; }

    /// <summary>
    /// 可选: 方便在agile配置中心后台对当前客户端进行查阅与管理
    /// </summary>
    public string AgileConfigName { get; set; }

    /// <summary>
    /// 可选: 	方便在agile配置中心后台对当前客户端进行查阅与管理
    /// </summary>
    public string AgileConfigTag { get; set; }

    /// <summary>
    /// 可选: 通过此配置决定拉取哪个环境的配置信息；如果不配置，服务端会默认返回第一个环境的配置
    /// </summary>
    public string AgileConfigEnv { get; set; }

    /// <summary>
    /// 可选: 如设置了此目录则将拉取到的配置项cache文件存储到该目录，否则直接存储到站点根目录
    /// </summary>
    public string AgileConfigCacheDirectory { get; set; }

    /// <summary>
    /// 可选: 配置 client 发送 http 请求的时候的超时时间，默认100s
    /// </summary>
    public string AgileConfigHttpTimeout { get; set; }

    public AgileConfig()
    {
        AgileConfigAppId = "";
        AgileConfigSecret = "";
        AgileConfigNodes = "";
        AgileConfigName = "";
        AgileConfigTag = "";
        AgileConfigEnv = "";
        AgileConfigCacheDirectory = "";
        AgileConfigHttpTimeout = "100";
    }
}