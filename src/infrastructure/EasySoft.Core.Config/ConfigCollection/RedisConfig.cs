using EasySoft.Core.Config.ConfigInterface;
using EasySoft.UtilityTools.Assists;

namespace EasySoft.Core.Config.ConfigCollection;

public class RedisConfig : IConfig
{
    public static readonly RedisConfig Instance = new();

    public string KeyPrefix { get; set; }

    public string Connections { get; set; }

    public string Sentinels { get; set; }

    public RedisConfig()
    {
        Connections = "";
        Sentinels = "";
        KeyPrefix = "";
    }
}