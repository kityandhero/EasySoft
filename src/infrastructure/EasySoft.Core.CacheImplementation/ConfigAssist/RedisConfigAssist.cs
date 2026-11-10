using EasySoft.Core.CacheImplementation.ConfigCollection;

namespace EasySoft.Core.CacheImplementation.ConfigAssist;

public static class RedisConfigAssist
{
    /// <summary>
    /// CacheConfigureNode
    /// </summary>
    public const string CacheConfigureNode = "Cache";

    /// <summary>
    /// CacheConfigureNode
    /// </summary>
    public const string CacheModeConfigureNode = "Mode";

    /// <summary>
    /// CacheConfigureNode
    /// </summary>
    public const string RedisConfigureNode = "Redis";

    public static string GetCacheMode()
    {
        return StringAssist.Join(
            [
                CacheConfigureNode,
                CacheModeConfigureNode
            ],
            ":"
        );
    }

    public static string GetRedisConnectionString()
    {
        return StringAssist.Join(
            [
                CacheConfigureNode,
                RedisConfigureNode,
                "ConnectionString"
            ],
            ":"
        );
    }

    public static string GetSentinelCollection()
    {
        return StringAssist.Join(
            [
                CacheConfigureNode,
                RedisConfigureNode,
                "Sentinel"
            ],
            ":"
        );
    }
}