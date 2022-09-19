using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class ConsulCenterConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(ConsulCenterConfig).ToLowerFirst()}.json";

    private static IConfiguration? Configuration { get; set; }

    private static IConfiguration ConsulConfiguration { get; set; }

    static ConsulCenterConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(ConsulCenterConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            true,
            true
        );

        ConsulConfiguration = builder.Build();

        ConsulConfiguration.Bind(ConsulCenterConfig.Instance);
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

    private static ConsulCenterConfig GetConsulConfig()
    {
        return ConsulCenterConfig.Instance;
    }

    public static string GetCenterAddress()
    {
        var v = GetConsulConfig().CenterAddress.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"You've enabled the configuration Center setting and set it to Consul type, please continue config CenterAddress, it in {ConfigFile} -> CenterAddress"
            );
        }

        return v;
    }
}