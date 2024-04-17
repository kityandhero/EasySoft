using EasySoft.Core.Config.Cap;
using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// GeneralConfig
/// </summary>
public class GeneralConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly GeneralConfig Instance = new();

    /// <summary>
    /// CacheMode
    /// </summary>
    public string CacheMode { get; set; } = CacheModeCollection.InMemory.ToString();

    #region Start Port

    /// <summary>
    /// HttpPort
    /// </summary>
    public string HttpPort { get; set; } = "0";

    /// <summary>
    /// HttpsPort
    /// </summary>
    public string HttpsPort { get; set; } = "0";

    #endregion

    /// <summary>
    /// HstsSwitch
    /// </summary>
    public string HstsSwitch { get; set; } = "0";

    /// <summary>
    /// 键名前缀，用于标记缓存键前缀, 队列名前缀等等, 以便于在复杂部署中隔离数据
    /// </summary>
    public string KeyPrefix { get; set; } = "";

    #region Compression

    /// <summary>
    /// 压缩传输开关
    /// </summary>
    public string CompressionSwitch { get; set; } = "0";

    #endregion

    #region Token

    /// <summary>
    /// 服务端使用缓存存储键值 token, 仅返回前端键名
    /// </summary>
    public string TokenServerDumpSwitch { get; set; } = "1";

    /// <summary>
    /// Http Header 中 Token 的键名, 默认 “token”
    /// </summary>
    public string TokenName { get; set; } = "token";

    /// <summary>
    /// 支持从Url解析Token, 特定场景下无法使用 Http Header 时候可开启, 有安全隐患
    /// </summary>
    public string TokenParseFromUrlSwitch { get; set; } = "0";

    /// <summary>
    /// 支持从Cookie解析Token, 使用场景为传统MVC开发等场景, 适用于单体部署
    /// </summary>
    public string TokenParseFromCookieSwitch { get; set; } = "0";

    /// <summary>
    /// 过期时间 (秒)
    /// </summary>
    public string TokenExpires { get; set; } = "7200";

    #region JsonWebToken

    /// <summary>
    /// Token的时间偏移量 (秒)
    /// </summary>
    public string JsonWebTokenClockSkew { get; set; } = "30";

    /// <summary>
    /// 访问群体
    /// </summary>
    public string JsonWebTokenValidAudience { get; set; } = "";

    /// <summary>
    /// 是否验证访问群体
    /// </summary>
    public string JsonWebTokenValidateAudience { get; set; } = "1";

    /// <summary>
    /// 颁发者
    /// </summary>
    public string JsonWebTokenValidIssuer { get; set; } = "";

    /// <summary>
    /// 是否验证颁发者
    /// </summary>
    public string JsonWebTokenValidateIssuer { get; set; } = "1";

    /// <summary>
    /// 是否验证生存期
    /// </summary>
    public string JsonWebTokenValidateLifetime { get; set; } = "1";

    /// <summary>
    /// 安全密钥
    /// </summary>
    public string JsonWebTokenIssuerSigningKey { get; set; } = "";

    /// <summary>
    /// 是否验证安全密钥
    /// </summary>
    public string JsonWebTokenValidateIssuerSigningKey { get; set; } = "1";

    #endregion

    #endregion

    #region AppSecurity

    public string AppId { get; set; } = "";

    public string AppSecret { get; set; } = "";

    public string AppSecurityServerHostUrl { get; set; } = "";

    #endregion

    public string AccessWayDetectSwitch { get; set; } = "0";

    public string RemoteGeneralLogSwitch { get; set; } = "0";

    public string RemoteErrorLogSwitch { get; set; } = "0";

    public string RemoteSqlExecutionRecordSwitch { get; set; } = "0";

    public string UseStaticFilesSwitch { get; set; } = "1";

    public string UseAuthentication { get; set; } = "0";

    public string UseAuthorization { get; set; } = "0";

    #region Cors

    public string CorsSwitch { get; set; } = "0";

    public string CorsPolicies { get; set; } = "*";

    #endregion

    public string HttpRedirectionHttpsSwitch { get; set; } = "0";

    /// <summary>
    /// 开关 反向代理场景下的转发 Http Header
    /// </summary>
    public string ForwardedHeadersSwitch { get; set; } = "0";

    #region Nog Embed Config

    #region Internal Log

    /// <summary>
    /// 内嵌Nlog内部日志的级别, Trace|Debug|Info|Warn|Error|Fatal|Off, Off 表示关闭
    /// </summary>
    public string NlogEmbedConfigInternalLogLevel { get; set; } = "Off";

    /// <summary>
    /// 开关 内嵌Nlog内部日志输出到文件开关
    /// </summary>
    public string NlogEmbedConfigInternalLogToFileSwitch { get; set; } = "0";

    /// <summary>
    /// 内嵌Nlog内部日志输出到的文件的路径
    /// </summary>
    public string NlogEmbedConfigInternalLogFile { get; set; } = "${basedir}/logs/nlog-internal.log";

    #endregion

    #region Production Log File

    /// <summary>
    /// 开关: 生产环境Nlog日志输出到文件
    /// </summary>
    public string NlogEmbedConfigProductionLogFileSwitch { get; set; } = "0";

    /// <summary>
    /// 生产环境Nlog日志的级别, Trace|Debug|Info|Warn|Error|Fatal
    /// </summary>
    public string NlogEmbedConfigProductionLogLevel { get; set; } = "Off";

    /// <summary>
    /// 生产环境Nlog当前日志输出目的地
    /// </summary>
    public string NlogEmbedConfigProductionLogFileName { get; set; } =
        "${basedir}/logs/nlog-production-${shortDate}.log";

    /// <summary>
    /// 生产环境Nlog存档日志输出目的地
    /// </summary>
    public string NlogEmbedConfigProductionLogArchiveFileName { get; set; } =
        "${basedir}/logs/nlog-production-${shortDate}-{#####}.log";

    #endregion

    /// <summary>
    /// 开关 内嵌Nlog配置中是否启用Trace日志记录, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogEmbedConfigTraceToFileSwitch { get; set; } = "0";

    /// <summary>
    /// 开关 内嵌Nlog配置中是否启用Debug日志记录, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogEmbedConfigDebugToFileSwitch { get; set; } = "0";

    /// <summary>
    /// 开关 内嵌Nlog配置中是否启用Trace日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogEmbedConfigTraceToConsoleSwitch { get; set; } = "0";

    /// <summary>
    /// 开关 内嵌Nlog配置中是否启用Debug日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    public string NlogEmbedConfigDebugToConsoleSwitch { get; set; } = "0";

    /// <summary>
    /// Nlog 控制台输出限额, 默认不详
    /// </summary>
    public string NlogEmbedConfigConsoleMessageLimit { get; set; } = "0";

    /// <summary>
    /// Nlog 控制台日志节流开关, 默认不启用
    /// </summary>
    public string NlogEmbedConfigConsoleLimitingWrapperSwitch { get; set; } = "0";

    /// <summary>
    /// Nlog 控制台忽略重复输出, 默认不启用
    /// </summary>
    public string NlogEmbedConfigConsoleRepeatedFilterSwitch { get; set; } = "0";

    #endregion

    public string MiniProFileSwitch { get; set; } = "0";

    public string WebRootPath { get; set; } = "";

    public string CapSwitch { get; set; } = "auto";

    public string CapPrefix { get; set; } = "EasySoft";

    public string CapTransportType { get; set; } = TransportType.InMemoryMessageQueue.ToString();

    public string CapPersistentType { get; set; } = PersistentType.ImMemory.ToString();

    public string CapPersistentConnection { get; set; } = "";

    public string CapDashboardSwitch { get; set; } = "0";

    public string CapDiscoverySwitch { get; set; } = "0";

    public string RegistrationCenterSwitch { get; set; } = "0";

    public string RegistrationCenterType { get; set; } = "";

    public string ConfigCenterSwitch { get; set; } = "0";

    public string ConfigCenterType { get; set; } = "";

    public string GatewaySwitch { get; set; } = "0";

    public string GatewayType { get; set; } = "";

    public string GatewayWithConsulSwitch { get; set; } = "0";

    public string GatewayConfigInConsulSwitch { get; set; } = "0";

    public string ExceptionlessSwitch { get; set; } = "0";

    public string ExceptionlessServerUrl { get; set; } = "";

    public string ExceptionlessApiKey { get; set; } = "";

    public string SkyApmSwitch { get; set; } = "0";

    public string PermissionServerHostUrl { get; set; } = "";
}