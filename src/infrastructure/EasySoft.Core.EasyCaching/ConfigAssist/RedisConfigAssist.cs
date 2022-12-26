using EasySoft.Core.EasyCaching.ConfigCollection;

namespace EasySoft.Core.EasyCaching.ConfigAssist;

public static class RedisConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(RedisConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static RedisConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(RedisConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(RedisConfig.Instance);
    }

    public static void Init()
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(RedisConfigAssist)}.{nameof(Init)}."
        );
    }

    /// <summary>
    /// 获取配置文件路径
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFilePath()
    {
        return FilePath;
    }

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
        return "[redisConfig.json](./configures/redisConfig.json)";
    }

    private static RedisConfig GetConfig()
    {
        return RedisConfig.Instance;
    }

    /// <summary>
    /// 获取服务配应用安装前缀配置
    /// </summary>
    public static List<string> GetConnectionCollection()
    {
        var v = GetConfig().Connections.Trim()
            .Split("|")
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        if (!v.Any())
            throw new ConfigErrorException(
                $"请配置Redis Connections: {ConfigFile} -> Connections,配置示例: 127.0.0.1:6388,defaultDatabase=13,poolsize=10|127.0.0.1:6389,defaultDatabase=13,poolsize=10",
                GetConfigFileInfo()
            );

        return v;
    }

    public static List<string> GetSentinelCollection()
    {
        var v = GetConfig().Sentinels.Trim()
            .Split("|")
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        return v;
    }

    public static string GetKeyPrefix()
    {
        var v = GetConfig().KeyPrefix;

        v = v.Remove(" ").Trim();

        return string.IsNullOrWhiteSpace(v) ? "" : v;
    }
}