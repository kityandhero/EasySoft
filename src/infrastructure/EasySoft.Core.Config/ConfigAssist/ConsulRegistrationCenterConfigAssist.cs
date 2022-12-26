using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// ConsulRegistrationCenterConfigAssist
/// </summary>
public static class ConsulRegistrationCenterConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(ConsulRegistrationCenterConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration? Configuration { get; set; }

    private static IConfiguration ConsulConfiguration { get; set; }

    static ConsulRegistrationCenterConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(ConsulRegistrationCenterConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        ConsulConfiguration = builder.Build();

        ConsulConfiguration.Bind(ConsulRegistrationCenterConfig.Instance);
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
    /// 获取配置文件信息
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    /// <summary>
    /// SetConfiguration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IConfiguration SetConfiguration(IConfiguration configuration)
    {
        Configuration = configuration;

        return Configuration;
    }

    /// <summary>
    /// GetConfiguration
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IConfiguration GetConfiguration()
    {
        if (Configuration == null) throw new Exception("ConsulConfig has not been established.");

        return Configuration;
    }

    /// <summary>
    /// GetSection
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IConfigurationSection GetSection(string key)
    {
        return GetConfiguration().GetSection(key);
    }

    /// <summary>
    /// GetValue
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetValue(string key)
    {
        return GetConfiguration().GetSection(key).Value;
    }

    private static ConsulRegistrationCenterConfig GetConsulConfig()
    {
        return ConsulRegistrationCenterConfig.Instance;
    }

    /// <summary>
    /// GetCenterAddress
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetCenterAddress()
    {
        var v = GetConsulConfig().CenterAddress.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"You've enabled the registration Center setting and set it to Consul type, please continue config CenterAddress, it in {ConfigFile} -> CenterAddress",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// GetServiceName
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetServiceName()
    {
        var v = GetConsulConfig().ServiceName.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置 ServiceName: {ConfigFile} -> ServiceName",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// GetServiceIP
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetServiceIP()
    {
        var v = GetConsulConfig().ServiceIP.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置 ServiceIP: {ConfigFile} -> ServiceIP",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// GetServicePort
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static int GetServicePort()
    {
        var v = GetConsulConfig().ServicePort.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v) || !v.IsInt(out var value))
            throw new ConfigErrorException(
                $"请配置 ServicePort: {ConfigFile} -> ServicePort",
                GetConfigFileInfo()
            );

        return value;
    }

    /// <summary>
    /// 服务停止多久后进行注销 (秒), 默认值 5
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int GetDeregisterCriticalServiceAfter()
    {
        var v = GetConsulConfig().DeregisterCriticalServiceAfter.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v) || !v.IsInt(out var value))
            throw new ConfigErrorException(
                $"请配置 DeregisterCriticalServiceAfter: {ConfigFile} -> DeregisterCriticalServiceAfter",
                GetConfigFileInfo()
            );

        return value;
    }

    /// <summary>
    /// 健康检查间隔,心跳间隔 (秒), 默认值 10
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int GetHealthCheckIntervalInSecond()
    {
        var v = GetConsulConfig().HealthCheckIntervalInSecond.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v) || !v.IsInt(out var value))
            throw new ConfigErrorException(
                $"请配置 HealthCheckIntervalInSecond: {ConfigFile} -> HealthCheckIntervalInSecond",
                GetConfigFileInfo()
            );

        return value;
    }

    /// <summary>
    /// 超时时间 (秒), 默认值 5
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int GetTimeout()
    {
        var v = GetConsulConfig().Timeout.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v) || !v.IsInt(out var value))
            throw new ConfigErrorException(
                $"请配置 Timeout: {ConfigFile} -> Timeout",
                GetConfigFileInfo()
            );

        return value;
    }

    /// <summary>
    /// GetServiceHealthCheck
    /// </summary>
    /// <returns></returns>
    public static string GetServiceHealthCheck()
    {
        var v = GetConsulConfig().ServiceHealthCheck.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }
}