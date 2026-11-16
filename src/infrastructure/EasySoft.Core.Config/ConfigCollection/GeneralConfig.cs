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

    #region AppSecurity

    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = "";

    /// <summary>
    /// AppSecret
    /// </summary>
    public string AppSecret { get; set; } = "";

    /// <summary>
    /// AppSecurityServerHostUrl
    /// </summary>
    public string AppSecurityServerHostUrl { get; set; } = "";

    #endregion

    /// <summary>
    /// AccessWayDetectSwitch
    /// </summary>
    public string AccessWayDetectSwitch { get; set; } = "0";

    /// <summary>
    /// RemoteGeneralLogSwitch
    /// </summary>
    public string RemoteGeneralLogSwitch { get; set; } = "0";

    /// <summary>
    /// UseStaticFilesSwitch
    /// </summary>
    public string UseStaticFilesSwitch { get; set; } = "1";

    /// <summary>
    /// UseAuthentication
    /// </summary>
    public string UseAuthentication { get; set; } = "0";

    /// <summary>
    /// UseAuthorization
    /// </summary>
    public string UseAuthorization { get; set; } = "0";

    #region Cors

    /// <summary>
    /// CorsSwitch
    /// </summary>
    public string CorsSwitch { get; set; } = "0";

    #endregion

    /// <summary>
    /// HttpRedirectionHttpsSwitch
    /// </summary>
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

    /// <summary>
    /// MiniProFileSwitch
    /// </summary>
    public string MiniProFileSwitch { get; set; } = "0";

    /// <summary>
    /// WebRootPath
    /// </summary>
    public string WebRootPath { get; set; } = "";

    /// <summary>
    /// CapSwitch
    /// </summary>
    public string CapSwitch { get; set; } = "auto";

    /// <summary>
    /// CapPrefix
    /// </summary>
    public string CapPrefix { get; set; } = "EasySoft";

    /// <summary>
    /// CapTransportType
    /// </summary>
    public string CapTransportType { get; set; } = TransportType.InMemoryMessageQueue.ToString();

    /// <summary>
    /// CapPersistentType
    /// </summary>
    public string CapPersistentType { get; set; } = PersistentType.ImMemory.ToString();

    /// <summary>
    /// CapPersistentConnection
    /// </summary>
    public string CapPersistentConnection { get; set; } = "";

    /// <summary>
    /// CapDashboardSwitch
    /// </summary>
    public string CapDashboardSwitch { get; set; } = "0";

    /// <summary>
    /// CapDiscoverySwitch
    /// </summary>
    public string CapDiscoverySwitch { get; set; } = "0";

    /// <summary>
    /// RegistrationCenterSwitch
    /// </summary>
    public string RegistrationCenterSwitch { get; set; } = "0";

    /// <summary>
    /// RegistrationCenterType
    /// </summary>
    public string RegistrationCenterType { get; set; } = "";

    /// <summary>
    /// ConfigCenterSwitch
    /// </summary>
    public string ConfigCenterSwitch { get; set; } = "0";

    /// <summary>
    /// ConfigCenterType
    /// </summary>
    public string ConfigCenterType { get; set; } = "";

    /// <summary>
    /// GatewaySwitch
    /// </summary>
    public string GatewaySwitch { get; set; } = "0";

    /// <summary>
    /// GatewayType
    /// </summary>
    public string GatewayType { get; set; } = "";

    /// <summary>
    /// GatewayWithConsulSwitch
    /// </summary>
    public string GatewayWithConsulSwitch { get; set; } = "0";

    /// <summary>
    /// GatewayConfigInConsulSwitch
    /// </summary>
    public string GatewayConfigInConsulSwitch { get; set; } = "0";

    /// <summary>
    /// SkyApmSwitch
    /// </summary>
    public string SkyApmSwitch { get; set; } = "0";

    /// <summary>
    /// PermissionServerHostUrl
    /// </summary>
    public string PermissionServerHostUrl { get; set; } = "";
}