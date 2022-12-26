using EasySoft.Core.Config.ExtensionMethods;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// AppSettingAssist
/// </summary>
public static class AppSettingAssist
{
    private const string ConfigFile = "appsettings.json";

    private static IConfiguration Configuration { get; set; }

    static AppSettingAssist()
    {
        var directory = AppContextAssist.GetBaseDirectory();

        var filePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(filePath).AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    /// <summary>
    /// Init
    /// </summary>
    public static void Init()
    {
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
        return Configuration.GetSection(key).Value;
    }
}