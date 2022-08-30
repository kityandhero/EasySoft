using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class AppSettingAssist
{
    private const string ConfigFile = "appsettings.json";

    private static IConfiguration Configuration { get; set; }

    static AppSettingAssist()
    {
        var directory = AppContextAssist.GetBaseDirectory();

        var filePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        ).AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    public static void Init()
    {
    }

    public static IConfiguration GetConfiguration()
    {
        return Configuration;
    }

    public static IConfigurationSection GetSection(string key)
    {
        return Configuration.GetSection(key);
    }

    public static string GetValue(string key)
    {
        return Configuration.GetSection(key).Value;
    }
}