using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Config.ConfigAssist;

public static class ConsulConfigCenterConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(ConsulConfigCenterConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration? Configuration { get; set; }

    private static IConfiguration ConsulConfiguration { get; set; }

    static ConsulConfigCenterConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(ConsulConfigCenterConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        ConsulConfiguration = builder.Build();

        ConsulConfiguration.Bind(ConsulConfigCenterConfig.Instance);
    }

    public static void Init()
    {
    }

    public static string GetConfigFilePath()
    {
        return FilePath;
    }

    public static async Task<string> GetConfigFileContent()
    {
        var content = await GetConfigFilePath().ReadFile();

        return string.IsNullOrWhiteSpace(content) ? content : JsonConvertAssist.FormatText(content);
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
        if (Configuration == null) throw new Exception("ConsulConfig has not been established.");

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

    private static ConsulConfigCenterConfig GetConsulConfig()
    {
        return ConsulConfigCenterConfig.Instance;
    }

    public static string GetCenterAddress()
    {
        var v = GetConsulConfig().CenterAddress.Trim();

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        if (string.IsNullOrWhiteSpace(v))
            throw new Exception(
                $"You've enabled the configuration Center setting and set it to Consul type, please continue config CenterAddress, it in {ConfigFile} -> CenterAddress"
            );

        return v;
    }
}