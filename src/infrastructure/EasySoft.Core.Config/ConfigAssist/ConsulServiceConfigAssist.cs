using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public class ConsulServiceConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(ConsulServiceConfig).ToLowerFirst()}.json";

    private static IConfiguration? Configuration { get; set; }

    private static IConfiguration ConsulConfiguration { get; set; }

    static ConsulServiceConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(ConsulServiceConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            true,
            true
        );

        ConsulConfiguration = builder.Build();

        ConsulConfiguration.Bind(ConsulServiceConfig.Instance);
    }

    public static void Init()
    {
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

    private static ConsulServiceConfig GetConsulConfig()
    {
        return ConsulServiceConfig.Instance;
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