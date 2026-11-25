using EasySoft.Core.Config.Cap;
using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// GeneralConfigAssist
/// </summary>
public static class GeneralConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(GeneralConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static GeneralConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(GeneralConfig.Instance);
    }

    /// <summary>
    /// Init
    /// </summary>
    public static void Init()
    {
    }

    /// <summary>
    /// 获取配置文件路径
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFilePath()
    {
        return FilePath;
    }

    /// <summary>
    /// 获取配置文件内容
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetConfigFileContent()
    {
        var content = await GetConfigFilePath().ReadFile();

        return string.IsNullOrWhiteSpace(content) ? content : JsonConvertAssist.FormatText(content);
    }

    /// <summary>
    /// GetConfigFileName
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFileName()
    {
        return ConfigFile;
    }

    /// <summary>
    /// 获取配置文件信息
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    /// <summary>
    /// GetConfiguration
    /// </summary>
    /// <returns></returns>
    public static IConfiguration GetConfiguration()
    {
        return Configuration;
    }

    /// <summary>
    /// GetSection
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IConfigurationSection GetSection(string key)
    {
        return Configuration.GetSection(key);
    }

    /// <summary>
    /// GetValue
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetValue(string key)
    {
        return Configuration.GetSection(key).Value ?? "";
    }

    private static GeneralConfig GetConfig()
    {
        return GeneralConfig.Instance;
    }

    /// <summary>
    /// GetId
    /// </summary>
    /// <returns></returns>
    public static string GetId()
    {
        var ticks = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var ticksHex = Convert.ToString(ticks, 16);
        var name = GetName();

        return $"{name}-{HostEnvironmentAssist.GetEnvironmentAliasName()}-{ticksHex}";
    }

    /// <summary>
    /// GetName
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetName()
    {
        var startAssembly = Assembly.GetEntryAssembly();

        var name = "";

        if (startAssembly != null)
        {
            var assemblyName = startAssembly.GetName();
            var fullName = assemblyName.Name?.ToLower() ?? "";

            name = fullName.Replace(".", "-");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("EntryAssembly not exist");
        }

        return name;
    }

    /// <summary>
    /// GetVersion
    /// </summary>
    /// <returns></returns>
    public static string GetVersion()
    {
        var startAssembly = Assembly.GetEntryAssembly();

        var version = "1.0.0.0";

        if (startAssembly == null)
        {
            return version;
        }

        var assemblyName = startAssembly.GetName();

        version = assemblyName.Version?.ToString() ?? "1.0.0.0";

        return version;
    }

    #region AppSecurity

    /// <summary>
    /// GetAppId
    /// </summary>
    /// <returns></returns>
    public static string GetAppId()
    {
        if (ApplicationSecurityServerConfigure.EmbedMode)
        {
            return AppSecurityAssist.EmbedAppId;
        }

        var v = GetConfig().AppId;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetAppSecret
    /// </summary>
    /// <returns></returns>
    public static string GetAppSecret()
    {
        if (ApplicationSecurityServerConfigure.EmbedMode)
        {
            return AppSecurityAssist.EmbedAppSecret;
        }

        var v = GetConfig().AppSecret;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetAppSecurityServerHostUrl
    /// </summary>
    /// <returns></returns>
    public static string GetAppSecurityServerHostUrl()
    {
        var v = GetConfig().AppSecurityServerHostUrl;

        if (!string.IsNullOrWhiteSpace(v))
        {
            return v;
        }

        v = FlagAssist.StartupDisplayUrls.First();

        StartupDescriptionMessageAssist.AddWarning(
            $"AppSecurityServerHostUrl has not config, use the start base url {v}.",
            GetConfigFileInfo()
        );

        return v;
    }

    #endregion

    /// <summary>
    /// GetAccessWayDetectSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetAccessWayDetectSwitch()
    {
        var v = GetConfig().AccessWayDetectSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 AccessWayDetectSwitch: {ConfigFile} -> AccessWayDetectSwitch,请设置 0/1,开启后将在请求进入时校验持久数据存在性",
                GetConfigFileInfo()
            );
        }

        if (value != 1)
        {
            return false;
        }

        if (!FlagAssist.PermissionVerificationSwitch)
        {
            throw new ConfigFileErrorException(
                "AccessWayDetectSwitch work with UsePermissionVerification, if you do not use UsePermissionVerification, do not set it to enable",
                GetConfigFileInfo()
            );
        }

        return true;
    }

    /// <summary>
    /// GetRemoteGeneralLogSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetRemoteGeneralLogSwitch()
    {
        var v = GetConfig().RemoteGeneralLogSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 RemoteGeneralLogSwitch: {ConfigFile} -> RemoteGeneralLogSwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetUseStaticFilesSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetUseStaticFilesSwitch()
    {
        var v = GetConfig().UseStaticFilesSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 UseStaticFiles: {ConfigFile} -> UseStaticFiles,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetUseAuthentication
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetUseAuthentication()
    {
        var v = GetConfig().UseAuthentication;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 UseAuthentication: {ConfigFile} -> UseAuthentication,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetUseAuthorization
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetUseAuthorization()
    {
        var v = GetConfig().UseAuthorization;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 UseAuthorization: {ConfigFile} -> UseAuthorization,请设置 0/1",
                GetConfigFileInfo()
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

    /// <summary>
    /// GetCapSwitch
    /// </summary>
    /// <returns></returns>
    public static string GetCapSwitch()
    {
        var v = GetConfig().CapSwitch.Remove(" ").Trim().ToLower();

        v = string.IsNullOrWhiteSpace(v) ? "auto" : v;

        return v;
    }

    /// <summary>
    /// CheckCapSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool CheckCapSwitch()
    {
        var v = GetConfig().CapSwitch.Remove(" ").Trim().ToLower();

        v = string.IsNullOrWhiteSpace(v) ? "auto" : v;

        if (!v.In(
                "0",
                "1",
                "auto"
            ))
        {
            throw new ConfigFileErrorException(
                $"请配置 CapSwitch: {ConfigFile} -> CapSwitch,请设置 0/1/auto",
                GetConfigFileInfo()
            );
        }

        if (v == 1.ToString())
        {
            return true;
        }

        if (v == "auto")
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// GetCapPrefix
    /// </summary>
    /// <returns></returns>
    public static string GetCapPrefix()
    {
        var v = GetConfig().CapPrefix.Remove(" ").Trim();

        return string.IsNullOrWhiteSpace(v) ? "" : v;
    }

    /// <summary>
    /// GetCapPersistentType
    /// </summary>
    /// <returns></returns>
    public static string GetCapPersistentType()
    {
        var v = GetConfig().CapPersistentType;

        v = string.IsNullOrWhiteSpace(v) ? PersistentType.ImMemory.ToString() : v;

        return v;
    }

    /// <summary>
    /// GetCapTransportType
    /// </summary>
    /// <returns></returns>
    public static string GetCapTransportType()
    {
        var v = GetConfig().CapTransportType;

        v = string.IsNullOrWhiteSpace(v) ? TransportType.InMemoryMessageQueue.ToString() : v;

        return v;
    }

    /// <summary>
    /// GetCapPersistentConnection
    /// </summary>
    /// <returns></returns>
    public static string GetCapPersistentConnection()
    {
        var v = GetConfig().CapPersistentConnection;

        return v;
    }

    /// <summary>
    /// GetCapDashboardSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetCapDashboardSwitch()
    {
        var v = GetConfig().CapDashboardSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 CapDashboardSwitch: {ConfigFile} -> CapDashboardSwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetCapDiscoverySwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetCapDiscoverySwitch()
    {
        var v = GetConfig().CapDiscoverySwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 CapDiscoverySwitch: {ConfigFile} -> CapDiscoverySwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetRegistrationCenterSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetRegistrationCenterSwitch()
    {
        var v = GetConfig().RegistrationCenterSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 RegistrationCenterSwitch: {ConfigFile} -> RegistrationCenterSwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetRegistrationCenterType
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static RegistrationCenterType GetRegistrationCenterType()
    {
        var v = GetConfig().RegistrationCenterType;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigFileErrorException(
                $"请配置 RegistrationCenterType: {ConfigFile} -> RegistrationCenterType,请设置 {RegistrationCenterType.Consul.ToString()}",
                GetConfigFileInfo()
            );
        }

        return Enum.Parse<RegistrationCenterType>(v);
    }

    /// <summary>
    /// GetConfigCenterSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetConfigCenterSwitch()
    {
        var v = GetConfig().ConfigCenterSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 ConfigCenterSwitch: {ConfigFile} -> ConfigCenterSwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetConfigCenterType
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static ConfigCenterType GetConfigCenterType()
    {
        var v = GetConfig().ConfigCenterType;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigFileErrorException(
                $"请配置 ConfigCenterType: {ConfigFile} -> ConfigCenterType,请设置 {ConfigCenterType.AgileConfig.ToString()}/{ConfigCenterType.Consul.ToString()}",
                GetConfigFileInfo()
            );
        }

        return Enum.Parse<ConfigCenterType>(v);
    }

    /// <summary>
    /// GetGatewaySwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetGatewaySwitch()
    {
        var v = GetConfig().GatewaySwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 GatewaySwitch: {ConfigFile} -> GatewaySwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetGatewayType
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static GatewayType GetGatewayType()
    {
        var v = GetConfig().GatewayType;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigFileErrorException(
                $"请配置 GatewayType: {ConfigFile} -> GatewayType,请设置 {GatewayType.Ocelot.ToString()}",
                GetConfigFileInfo()
            );
        }

        return Enum.Parse<GatewayType>(v);
    }

    /// <summary>
    /// GetGatewayWithConsulSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetGatewayWithConsulSwitch()
    {
        var v = GetConfig().GatewayWithConsulSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 GatewayWithConsulSwitch: {ConfigFile} -> GatewayWithConsulSwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetGatewayConfigInConsulSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetGatewayConfigInConsulSwitch()
    {
        if (!GetGatewayWithConsulSwitch())
        {
            return false;
        }

        var v = GetConfig().GatewayConfigInConsulSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 GatewayConfigInConsulSwitch: {ConfigFile} -> GatewayConfigInConsulSwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetSkyApmSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileErrorException"></exception>
    public static bool GetSkyApmSwitch()
    {
        var v = GetConfig().SkyApmSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt(out var value))
        {
            throw new ConfigFileErrorException(
                $"请配置 SkyApmSwitch: {ConfigFile} -> SkyApmSwitch,请设置 0/1",
                GetConfigFileInfo()
            );
        }

        return value == 1;
    }

    /// <summary>
    /// GetPermissionServerHostUrl
    /// </summary>
    /// <returns></returns>
    public static string GetPermissionServerHostUrl()
    {
        var v = GetConfig().PermissionServerHostUrl;

        if (!string.IsNullOrWhiteSpace(v))
        {
            return v;
        }

        v = FlagAssist.StartupDisplayUrls.First();

        StartupDescriptionMessageAssist.AddWarning(
            $"PermissionServerHostUrl has not config, use the start base url {v}.",
            GetConfigFileInfo()
        );

        return v;
    }
}