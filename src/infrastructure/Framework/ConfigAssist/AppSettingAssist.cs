using Framework.Utils;
using Microsoft.Extensions.Configuration;

namespace Framework.ConfigAssist;

public class AppSettingAssist
{
    private const string ConfigFile = $"appsettings.json";

    private static IConfiguration Configuration { get; set; }

    static AppSettingAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();
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