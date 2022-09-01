using EasySoft.Core.Config.ConfigInterface;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.Core.Config.ConfigCollection;

public class GeneralConfig : IConfig
{
    public static readonly GeneralConfig Instance = new();

    public string CacheMode { get; set; }

    public string Urls { get; set; }

    #region Token

    /// <summary>
    /// 服务端使用缓存存储键值 token, 仅返回前端键名
    /// </summary>
    public string TokenServerDumpSwitch { get; set; }

    public string TokenName { get; set; }

    /// <summary>
    /// 支持从Url解析Token, 特定场景下无法使用 Http Header 时候可开启, 有安全隐患
    /// </summary>
    public string TokenParseFromUrlSwitch { get; set; }

    /// <summary>
    /// 支持从Cookie解析Token, 使用场景为传统MVC开发等场景, 适用于单体部署
    /// </summary>
    public string TokenParseFromCookieSwitch { get; set; }

    /// <summary>
    /// 过期时间 (秒)
    /// </summary>
    public string TokenExpires { get; set; }

    #region JsonWebToken

    /// <summary>
    /// Token的时间偏移量 (秒)
    /// </summary>
    public string JsonWebTokenClockSkew { get; set; }

    /// <summary>
    /// 访问群体
    /// </summary>
    public string JsonWebTokenValidAudience { get; set; }

    /// <summary>
    /// 是否验证访问群体
    /// </summary>
    public string JsonWebTokenValidateAudience { get; set; }

    /// <summary>
    /// 颁发者
    /// </summary>
    public string JsonWebTokenValidIssuer { get; set; }

    /// <summary>
    /// 是否验证颁发者
    /// </summary>
    public string JsonWebTokenValidateIssuer { get; set; }

    /// <summary>
    /// 是否验证生存期
    /// </summary>
    public string JsonWebTokenValidateLifetime { get; set; }

    /// <summary>
    /// 安全密钥
    /// </summary>
    public string JsonWebTokenIssuerSigningKey { get; set; }

    /// <summary>
    /// 是否验证安全密钥
    /// </summary>
    public string JsonWebTokenValidateIssuerSigningKey { get; set; }

    #endregion

    #endregion

    public string AccessWayDetectSwitch { get; set; }

    public string RemoteGeneralLogSwitch { get; set; }

    public string RemoteErrorLogSwitch { get; set; }

    public string RemoteSqlExecutionRecordSwitch { get; set; }

    public string UseStaticFilesSwitch { get; set; }

    public string UseAuthentication { get; set; }

    public string UseAuthorization { get; set; }

    public string CorsSwitch { get; set; }

    public string CorsPolicies { get; set; }

    public string HttpRedirectionHttpsSwitch { get; set; }

    #region AgileConfig

    /// <summary>
    /// 开关: 是否链接 AgileConfig
    /// </summary>
    public string AgileConfigSwitch { get; set; }

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

    #endregion

    /// <summary>
    /// 开关 反向代理场景下的转发 Http Header
    /// </summary>
    public string ForwardedHeadersSwitch { get; set; }

    /// <summary>
    /// 开关 默认Nlog配置中是否启用Trace日志记录, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogDefaultConfigTraceToFileSwitch { get; set; }

    /// <summary>
    /// 开关 默认Nlog配置中是否启用Debug日志记录, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogDefaultConfigDebugToFileSwitch { get; set; }

    /// <summary>
    /// 开关 默认Nlog配置中是否启用Trace日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogDefaultConfigTraceToConsoleSwitch { get; set; }

    /// <summary>
    /// 开关 默认Nlog配置中是否启用Debug日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogDefaultConfigDebugToConsoleSwitch { get; set; }

    public string WebRootPath { get; set; }

    public GeneralConfig()
    {
        CacheMode = CacheModeCollection.InMemory.ToString();

        Urls = "";

        AccessWayDetectSwitch = "0";
        RemoteGeneralLogSwitch = "0";
        RemoteErrorLogSwitch = "0";
        RemoteSqlExecutionRecordSwitch = "0";

        UseStaticFilesSwitch = "1";

        UseAuthentication = "0";
        UseAuthorization = "0";

        CorsSwitch = "0";
        CorsPolicies = "*";

        TokenServerDumpSwitch = "1";
        TokenExpires = "7200";
        TokenParseFromUrlSwitch = "0";
        TokenParseFromCookieSwitch = "0";
        TokenName = "token";

        JsonWebTokenClockSkew = "30";
        JsonWebTokenValidateAudience = "1";
        JsonWebTokenValidateIssuer = "1";
        JsonWebTokenValidateLifetime = "1";
        JsonWebTokenValidateIssuerSigningKey = "1";
        JsonWebTokenValidAudience = "";
        JsonWebTokenValidIssuer = "";
        JsonWebTokenIssuerSigningKey = "";

        ForwardedHeadersSwitch = "0";

        AgileConfigSwitch = "0";
        AgileConfigAppId = "";
        AgileConfigSecret = "";
        AgileConfigNodes = "";
        AgileConfigName = "";
        AgileConfigTag = "";
        AgileConfigEnv = "";
        AgileConfigCacheDirectory = "";
        AgileConfigHttpTimeout = "100";

        HttpRedirectionHttpsSwitch = "0";

        NlogDefaultConfigTraceToFileSwitch = "0";
        NlogDefaultConfigDebugToFileSwitch = "0";

        NlogDefaultConfigTraceToConsoleSwitch = "1";
        NlogDefaultConfigDebugToConsoleSwitch = "1";

        WebRootPath = "";
    }
}