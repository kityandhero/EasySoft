using EasySoft.Core.Config.Cap;
using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class GeneralConfig : IConfig
{
    public static readonly GeneralConfig Instance = new();

    public string CacheMode { get; set; }

    #region Start Port

    public string HttpPort { get; set; }

    public string HttpsPort { get; set; }

    #endregion

    public string HstsSwitch { get; set; }

    #region Compression

    public string CompressionSwitch { get; set; }

    #endregion

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

    #region Cors

    public string CorsSwitch { get; set; }

    public string CorsPolicies { get; set; }

    #endregion

    public string HttpRedirectionHttpsSwitch { get; set; }

    /// <summary>
    /// 开关 反向代理场景下的转发 Http Header
    /// </summary>
    public string ForwardedHeadersSwitch { get; set; }

    #region Nog Embed Config

    #region Internal Log

    /// <summary>
    /// 内嵌Nlog内部日志的级别, Trace|Debug|Info|Warn|Error|Fatal|Off, Off 表示关闭
    /// </summary>
    public string NlogEmbedConfigInternalLogLevel { get; set; }

    /// <summary>
    /// 开关 内嵌Nlog内部日志输出到文件开关
    /// </summary>
    public string NlogEmbedConfigInternalLogToFileSwitch { get; set; }

    /// <summary>
    /// 内嵌Nlog内部日志输出到的文件的路径
    /// </summary>
    public string NlogEmbedConfigInternalLogFile { get; set; }

    #endregion

    #region Production Log File

    /// <summary>
    /// 开关: 生产环境Nlog日志输出到文件
    /// </summary>
    public string NlogEmbedConfigProductionLogFileSwitch { get; set; }

    /// <summary>
    /// 生产环境Nlog日志的级别, Trace|Debug|Info|Warn|Error|Fatal
    /// </summary>
    public string NlogEmbedConfigProductionLogLevel { get; set; }

    /// <summary>
    /// 生产环境Nlog当前日志输出目的地
    /// </summary>
    public string NlogEmbedConfigProductionLogFileName { get; set; }

    /// <summary>
    /// 生产环境Nlog存档日志输出目的地
    /// </summary>
    public string NlogEmbedConfigProductionLogArchiveFileName { get; set; }

    #endregion

    /// <summary>
    /// 开关 内嵌Nlog配置中是否启用Trace日志记录, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogEmbedConfigTraceToFileSwitch { get; set; }

    /// <summary>
    /// 开关 内嵌Nlog配置中是否启用Debug日志记录, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogEmbedConfigDebugToFileSwitch { get; set; }

    /// <summary>
    /// 开关 内嵌Nlog配置中是否启用Trace日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogEmbedConfigTraceToConsoleSwitch { get; set; }

    /// <summary>
    /// 开关 内嵌Nlog配置中是否启用Debug日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogEmbedConfigDebugToConsoleSwitch { get; set; }

    /// <summary>
    /// Nlog 控制台输出限额, 默认不详
    /// </summary>
    public string NlogEmbedConfigConsoleMessageLimit { get; set; }

    /// <summary>
    /// Nlog 控制台日志节流开关, 默认不启用
    /// </summary>
    public string NlogEmbedConfigConsoleLimitingWrapperSwitch { get; set; }

    /// <summary>
    /// Nlog 控制台忽略重复输出, 默认不启用
    /// </summary>
    public string NlogEmbedConfigConsoleRepeatedFilterSwitch { get; set; }

    #endregion

    public string MiniProFileSwitch { get; set; }

    public string WebRootPath { get; set; }

    public string CapSwitch { get; set; }

    public string CapPrefix { get; set; }

    public string CapTransportType { get; set; }

    public string CapPersistentType { get; set; }

    public string CapPersistentConnection { get; set; }

    public string CapDashboardSwitch { get; set; }

    public string CapDiscoverySwitch { get; set; }

    public string RegistrationCenterSwitch { get; set; }

    public string RegistrationCenterType { get; set; }

    public string ConfigCenterSwitch { get; set; }

    public string ConfigCenterType { get; set; }

    public string GatewaySwitch { get; set; }

    public string GatewayType { get; set; }

    public string GatewayWithConsulSwitch { get; set; }

    public string GatewayConfigInConsulSwitch { get; set; }

    public string ExceptionlessSwitch { get; set; }

    public string ExceptionlessServerUrl { get; set; }

    public string ExceptionlessApiKey { get; set; }

    public string SkyApmSwitch { get; set; }

    public string PermissionServerHostUrl { get; set; }

    public GeneralConfig()
    {
        CacheMode = CacheModeCollection.InMemory.ToString();

        HttpPort = "0";
        HttpsPort = "0";

        HstsSwitch = "0";

        CompressionSwitch = "0";

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

        #region NlogEmbedConfig

        #region Internal Log

        NlogEmbedConfigInternalLogLevel = "Off";
        NlogEmbedConfigInternalLogToFileSwitch = "0";
        NlogEmbedConfigInternalLogFile = "${basedir}/logs/nlog-internal.log";

        #endregion

        #region Production Log File

        NlogEmbedConfigProductionLogFileSwitch = "0";
        NlogEmbedConfigProductionLogLevel = "Off";
        NlogEmbedConfigProductionLogFileName = "${basedir}/logs/nlog-production-${shortDate}.log";
        NlogEmbedConfigProductionLogArchiveFileName = "${basedir}/logs/nlog-production-${shortDate}-{#####}.log";

        #endregion

        NlogEmbedConfigTraceToFileSwitch = "0";
        NlogEmbedConfigDebugToFileSwitch = "0";
        NlogEmbedConfigTraceToConsoleSwitch = "0";
        NlogEmbedConfigDebugToConsoleSwitch = "0";
        NlogEmbedConfigConsoleMessageLimit = "0";
        NlogEmbedConfigConsoleLimitingWrapperSwitch = "0";
        NlogEmbedConfigConsoleRepeatedFilterSwitch = "0";

        #endregion

        MiniProFileSwitch = "0";

        WebRootPath = "";

        CapSwitch = "auto";
        CapPrefix = "EasySoft";
        CapTransportType = TransportType.InMemoryMessageQueue.ToString();
        CapPersistentType = PersistentType.ImMemory.ToString();
        CapPersistentConnection = "";
        CapDashboardSwitch = "0";
        CapDiscoverySwitch = "0";

        RegistrationCenterSwitch = "0";
        RegistrationCenterType = "";
        ConfigCenterSwitch = "0";
        ConfigCenterType = "";

        GatewaySwitch = "0";
        GatewayType = "";
        GatewayWithConsulSwitch = "0";
        GatewayConfigInConsulSwitch = "0";

        ExceptionlessSwitch = "0";
        ExceptionlessServerUrl = "";
        ExceptionlessApiKey = "";

        SkyApmSwitch = "0";

        PermissionServerHostUrl = "";
    }
}