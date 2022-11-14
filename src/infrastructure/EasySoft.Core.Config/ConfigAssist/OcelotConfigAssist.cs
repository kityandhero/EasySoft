using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Config.ConfigAssist;

public static class OcelotConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(OcelotConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static OcelotConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();
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
        var content = await FilePath.ReadFile();

        return string.IsNullOrWhiteSpace(content) ? content : JsonConvertAssist.FormatText(content);
    }

    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    public static IConfiguration GetConfiguration()
    {
        return Configuration;
    }
}