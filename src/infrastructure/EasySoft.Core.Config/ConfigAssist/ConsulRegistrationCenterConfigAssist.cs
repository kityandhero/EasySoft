using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class ConsulRegistrationCenterConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(ConsulRegistrationCenterConfig).ToLowerFirst()}.json";

    private static IConfiguration? Configuration { get; set; }

    private static IConfiguration ConsulConfiguration { get; set; }

    static ConsulRegistrationCenterConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(ConsulRegistrationCenterConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            true,
            true
        );

        ConsulConfiguration = builder.Build();

        ConsulConfiguration.Bind(ConsulRegistrationCenterConfig.Instance);
    }

    public static void Init()
    {
    }

    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    public static IConfiguration SetConfiguration(IConfiguration configuration)
    {
        Configuration = configuration;

        return Configuration;
    }

    public static IConfiguration GetConfiguration()
    {
        if (Configuration == null)
        {
            throw new Exception("ConsulConfig has not been established.");
        }

        return Configuration;
    }

    public static IConfigurationSection GetSection(string key)
    {
        return GetConfiguration().GetSection(key);
    }

    public static string GetValue(string key)
    {
        return GetConfiguration().GetSection(key).Value;
    }

    private static ConsulRegistrationCenterConfig GetConsulConfig()
    {
        return ConsulRegistrationCenterConfig.Instance;
    }

    public static string GetCenterAddress()
    {
        var v = GetConsulConfig().CenterAddress.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"You've enabled the registration Center setting and set it to Consul type, please continue config CenterAddress, it in {ConfigFile} -> CenterAddress"
            );
        }

        return v;
    }

    public static string GetServiceName()
    {
        var v = GetConsulConfig().ServiceName.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置 ServiceName: {ConfigFile} -> ServiceName"
            );
        }

        return v;
    }

    public static string GetServiceIP()
    {
        var v = GetConsulConfig().ServiceIP.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置 ServiceIP: {ConfigFile} -> ServiceIP"
            );
        }

        return v;
    }

    public static int GetServicePort()
    {
        var v = GetConsulConfig().ServicePort.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v) || !v.IsInt(out var value))
        {
            throw new Exception(
                $"请配置 ServicePort: {ConfigFile} -> ServicePort"
            );
        }

        return value;
    }

    public static string GetServiceHealthCheck()
    {
        var v = GetConsulConfig().ServiceHealthCheck.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }
}