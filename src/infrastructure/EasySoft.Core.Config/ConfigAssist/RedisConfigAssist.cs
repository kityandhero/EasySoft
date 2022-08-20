using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class RedisConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(RedisConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static RedisConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(RedisConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(RedisConfig.Instance);
    }

    public static void Init()
    {
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
        {
            throw new Exception(
                $"请配置Redis Connections: {ConfigFile} -> Connections,配置示例: 127.0.0.1:6388,defaultDatabase=13,poolsize=10|127.0.0.1:6389,defaultDatabase=13,poolsize=10"
            );
        }

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