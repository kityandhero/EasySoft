using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class GeneralConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(GeneralConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static GeneralConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            true,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(GeneralConfig.Instance);
    }

    public static void Init()
    {
    }

    public static bool GetRemoteLogSwitch()
    {
        return GetRemoteErrorLogSwitch() || GetRemoteGeneralLogSwitch();
    }

    private static GeneralConfig GetConfig()
    {
        return GeneralConfig.Instance;
    }

    public static string GetUrls()
    {
        var v = GetConfig().Urls;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    public static string GetCacheMode()
    {
        var v = GetConfig().CacheMode;

        v = string.IsNullOrWhiteSpace(v) ? "InMemory" : v;

        if (!v.In("InMemory", "Redis"))
        {
            throw new Exception(
                $"请配置 CacheMode: {ConfigFile} -> CacheMode,请设置 InMemory/Redis"
            );
        }

        return v;
    }

    public static bool GetAccessWayDetectSwitch()
    {
        var v = GetConfig().AccessWayDetectSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 AccessWayDetectSwitch: {ConfigFile} -> AccessWayDetectSwitch,请设置 0/1,开启后将在请求进入时校验持久数据存在性"
            );
        }

        if (value != 1)
        {
            return false;
        }

        if (!FlagAssist.PermissionVerificationSwitch)
        {
            throw new Exception(
                "AccessWayDetectSwitch work with UsePermissionVerification, if you do not use UsePermissionVerification, do not set it to enable"
            );
        }

        return true;
    }

    public static bool GetRemoteGeneralLogSwitch()
    {
        var v = GetConfig().RemoteGeneralLogSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 RemoteGeneralLogSwitch: {ConfigFile} -> RemoteGeneralLogSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    public static bool GetRemoteErrorLogSwitch()
    {
        var v = GetConfig().RemoteErrorLogSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 RemoteErrorLogSwitch: {ConfigFile} -> RemoteErrorLogSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    public static bool GetRemoteSqlExecutionRecordSwitch()
    {
        var v = GetConfig().RemoteSqlExecutionRecordSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 RemoteSqlExecutionRecordSwitch: {ConfigFile} -> RemoteSqlExecutionRecordSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    public static bool GetUseStaticFilesSwitch()
    {
        var v = GetConfig().UseStaticFilesSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 UseStaticFiles: {ConfigFile} -> UseStaticFiles,请设置 0/1"
            );
        }

        return value == 1;
    }

    public static bool GetUseAuthentication()
    {
        var v = GetConfig().UseAuthentication;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 UseAuthentication: {ConfigFile} -> UseAuthentication,请设置 0/1"
            );
        }

        return value == 1;
    }

    public static bool GetUseAuthorization()
    {
        var v = GetConfig().UseAuthorization;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 UseAuthorization: {ConfigFile} -> UseAuthorization,请设置 0/1"
            );
        }

        return value == 1;
    }

    public static bool GetCorsSwitch()
    {
        var v = GetConfig().CorsSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 CorsSwitch: {ConfigFile} -> CorsSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    public static List<string> GetCorsPolicies()
    {
        var v = GetConfig().CorsPolicies.Trim()
            .Split(",")
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        return v;
    }

    /// <summary>
    /// 开关: 反代代理场景下的转发 Http Header 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool GetForwardedHeadersSwitch()
    {
        var v = GetConfig().ForwardedHeadersSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 ForwardedHeadersSwitch: {ConfigFile} -> ForwardedHeadersSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    #region Token

    /// <summary>
    /// 服务端使用缓存存储键值 token, 仅返回前端键名
    /// </summary>
    /// <returns></returns>
    public static bool GetTokenServerDumpSwitch()
    {
        var v = GetConfig().TokenServerDumpSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 TokenServerDumpSwitch: {ConfigFile} -> TokenServerDumpSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 支持从Url解析Token, 特定场景下无法使用 Http Header 时候可开启, 有安全隐患
    /// </summary>
    /// <returns></returns>
    public static bool GetTokenParseFromUrlSwitch()
    {
        var v = GetConfig().TokenParseFromUrlSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 TokenParseFromUrlSwitch: {ConfigFile} -> TokenParseFromUrlSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 支持从Cookie解析Token, 使用场景为传统MVC开发等场景, 适用于单体部署
    /// </summary>
    /// <returns></returns>
    public static bool GetTokenParseFromCookieSwitch()
    {
        var v = GetConfig().TokenParseFromCookieSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 TokenParseFromCookieSwitch: {ConfigFile} -> TokenParseFromCookieSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 过期时间 (秒), 默认7200
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int GetTokenExpires()
    {
        var v = GetConfig().TokenExpires;

        v = string.IsNullOrWhiteSpace(v) ? "7200" : v;

        if (!v.IsInt(out var value) || value < 0)
        {
            throw new Exception(
                $"请配置 JsonWebTokenExpires: {ConfigFile} -> JsonWebTokenExpires,请设置数字 value > 0"
            );
        }

        return value;
    }

    public static string GetTokenName()
    {
        var v = GetConfig().TokenName.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "token" : v;

        return v;
    }

    #region JsonWebToken

    /// <summary>
    /// Token的时间偏移量 (秒)
    /// </summary>
    /// <returns></returns>
    public static int GetJsonWebTokenClockSkew()
    {
        var v = GetConfig().JsonWebTokenClockSkew;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value) || value < 0)
        {
            throw new Exception(
                $"请配置 JsonWebTokenClockSkew: {ConfigFile} -> JsonWebTokenClockSkew,请设置数字 value >= 0"
            );
        }

        return value;
    }

    /// <summary>
    /// 颁发者
    /// </summary>
    /// <returns></returns>
    public static string GetJsonWebTokenValidIssuer()
    {
        var v = GetConfig().JsonWebTokenValidIssuer.Remove(" ").Trim();

        if (string.IsNullOrEmpty(v))
        {
            throw new Exception(
                $"请配置 JsonWebTokenValidIssuer: {ConfigFile} -> JsonWebTokenValidIssuer,请设置颁发者"
            );
        }

        return v;
    }

    /// <summary>
    /// 是否验证颁发者, 默认开启
    /// </summary>
    /// <returns></returns>
    public static bool GetJsonWebTokenValidateIssuer()
    {
        var v = GetConfig().JsonWebTokenValidateIssuer;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 JsonWebTokenValidateIssuer: {ConfigFile} -> JsonWebTokenValidateIssuer,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 访问群体
    /// </summary>
    /// <returns></returns>
    public static string GetJsonWebTokenValidAudience()
    {
        var v = GetConfig().JsonWebTokenValidAudience.Remove(" ").Trim();

        if (string.IsNullOrEmpty(v))
        {
            throw new Exception(
                $"请配置 JsonWebTokenValidAudience: {ConfigFile} -> JsonWebTokenValidAudience,请设置访问群体"
            );
        }

        return v;
    }

    /// <summary>
    /// 是否验证访问群体, 默认开启
    /// </summary>
    /// <returns></returns>
    public static bool GetJsonWebTokenValidateAudience()
    {
        var v = GetConfig().JsonWebTokenValidateAudience;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 JsonWebTokenValidateAudience: {ConfigFile} -> JsonWebTokenValidateAudience, 请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 安全密钥
    /// </summary>
    /// <returns></returns>
    public static string GetJsonWebTokenIssuerSigningKey()
    {
        var v = GetConfig().JsonWebTokenIssuerSigningKey.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置 JsonWebTokenIssuerSigningKey: {ConfigFile} -> JsonWebTokenIssuerSigningKey"
            );
        }

        return v;
    }

    /// <summary>
    /// 是否验证安全密钥, 默认开启
    /// </summary>
    /// <returns></returns>
    public static bool GetJsonWebTokenValidateIssuerSigningKey()
    {
        var v = GetConfig().JsonWebTokenValidateIssuerSigningKey;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 JsonWebTokenValidateIssuerSigningKey: {ConfigFile} -> JsonWebTokenValidateIssuerSigningKey,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 是否验证生存期, 默认开启
    /// </summary>
    /// <returns></returns>
    public static bool GetJsonWebTokenValidateLifetime()
    {
        var v = GetConfig().JsonWebTokenValidateLifetime;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 JsonWebTokenValidateLifetime: {ConfigFile} -> JsonWebTokenValidateLifetime,请设置 0/1"
            );
        }

        return value == 1;
    }

    #endregion

    #endregion

    #region AgileConfig

    /// <summary>
    /// 开关: 是否链接 AgileConfig
    /// </summary>
    /// <returns></returns>
    public static bool GetAgileConfigSwitch()
    {
        var v = GetConfig().AgileConfigSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 AgileConfigSwitch: {ConfigFile} -> AgileConfigSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 启用时必填: 后台管理中应用的应用ID
    /// </summary>
    /// <returns></returns>
    public static string GetAgileConfigAppId()
    {
        var v = GetConfig().AgileConfigAppId.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (GetAgileConfigSwitch() && string.IsNullOrWhiteSpace(v))
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

        if (GetAgileConfigSwitch() && string.IsNullOrWhiteSpace(v))
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

        if (GetAgileConfigSwitch() && string.IsNullOrWhiteSpace(v))
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

    #endregion

    /// <summary>
    /// 开关: 是否将Http请求重定向为Https, 即是否使用 UseHttpsRedirection, 默认关闭
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool GetHttpRedirectionHttpsSwitch()
    {
        var v = GetConfig().HttpRedirectionHttpsSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 HttpRedirectionHttpsSwitch: {ConfigFile} -> HttpRedirectionHttpsSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 开关: 默认Nlog配置中是否启用Trace日志写入, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool GetNlogDefaultConfigTraceToFileSwitch()
    {
        var v = GetConfig().NlogDefaultConfigTraceToFileSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 NlogDefaultConfigTraceToFileSwitch: {ConfigFile} -> NlogDefaultConfigTraceToFileSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 开关: 默认Nlog配置中是否启用Trace日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool GetNlogDefaultConfigTraceToConsoleSwitch()
    {
        var v = GetConfig().NlogDefaultConfigTraceToConsoleSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 NlogDefaultConfigTraceToConsoleSwitch: {ConfigFile} -> NlogDefaultConfigTraceToConsoleSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 开关: 默认Nlog配置中是否启用Debug日志写入, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool GetNlogDefaultConfigDebugToFileSwitch()
    {
        var v = GetConfig().NlogDefaultConfigDebugToFileSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 NlogDefaultConfigDebugToFileSwitch: {ConfigFile} -> NlogDefaultConfigDebugToFileSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 开关: 默认Nlog配置中是否启用Debug日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// NlogDefaultConfigTraceToConsoleSwitch 启用的情况下, 该开关将强制启用
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool GetNlogDefaultConfigDebugToConsoleSwitch()
    {
        if (GetNlogDefaultConfigTraceToConsoleSwitch())
        {
            return true;
        }

        var v = GetConfig().NlogDefaultConfigDebugToConsoleSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 NlogDefaultConfigDebugToConsoleSwitch: {ConfigFile} -> NlogDefaultConfigDebugToConsoleSwitch,请设置 0/1"
            );
        }

        return value == 1;
    }

    /// <summary>
    /// 开关: 默认Nlog配置中是否启用Debug日志记录, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetWebRootPath()
    {
        var v = GetConfig().WebRootPath;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }
}