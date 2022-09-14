using EasySoft.Core.Config.Cap;
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

    public string MiniProFileSwitch { get; set; }

    public string WebRootPath { get; set; }

    public string CapSwitch { get; set; }

    public string CapPrefix { get; set; }

    public string CapTransportType { get; set; }

    public string CapPersistentType { get; set; }

    public string CapDashboardSwitch { get; set; }

    public string CapDiscoverySwitch { get; set; }

    public string RegistrationCenterSwitch { get; set; }

    public string RegistrationCenterType { get; set; }
    
    public string ConfigCenterSwitch { get; set; }

    public string ConfigCenterType { get; set; }

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

        HttpRedirectionHttpsSwitch = "0";

        NlogDefaultConfigTraceToFileSwitch = "0";
        NlogDefaultConfigDebugToFileSwitch = "0";

        NlogDefaultConfigTraceToConsoleSwitch = "1";
        NlogDefaultConfigDebugToConsoleSwitch = "1";

        MiniProFileSwitch = "0";

        WebRootPath = "";

        CapSwitch = "auto";
        CapPrefix = "EasySoft";
        CapTransportType = TransportType.InMemoryMessageQueue.ToString();
        CapPersistentType = PersistentType.ImMemory.ToString();
        CapDashboardSwitch = "0";
        CapDiscoverySwitch = "0";

        RegistrationCenterSwitch = "0";
        RegistrationCenterType = "";
        ConfigCenterSwitch = "0";
        ConfigCenterType = "";
    }
}