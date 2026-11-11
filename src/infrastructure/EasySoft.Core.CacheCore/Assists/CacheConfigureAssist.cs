using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.CacheCore.Assists;

/// <summary>
/// CacheConfigureAssist
/// </summary>
public class CacheConfigureAssist
{
    #region Fields

    /// <summary>
    /// CacheConfigureNode
    /// </summary>
    private const string CacheConfigureNode = "Cache";

    /// <summary>
    /// CacheConfigureNode
    /// </summary>
    private const string CacheModeConfigureNode = "Mode";

    /// <summary>
    /// CacheKeyPrefixConfigureNode
    /// </summary>
    private const string CacheKeyPrefixConfigureNode = "KeyPrefix";

    /// <summary>
    /// CacheConfigureNode
    /// </summary>
    private const string RedisConfigureNode = "Redis";

    #endregion

    #region Properties

    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    public CacheConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    public string GetCacheMode()
    {
        var node = StringAssist.Join(
            [
                CacheConfigureNode,
                CacheModeConfigureNode
            ],
            ":"
        );

        return Configuration[node]
               ?? throw new InvalidOperationException(
                   $"缺少 Cache Mode 配置 inMemory/redis [{node}]。"
               );
    }

    public string GetCacheKeyPrefix()
    {
        var node = StringAssist.Join(
            [
                CacheConfigureNode,
                CacheKeyPrefixConfigureNode
            ],
            ":"
        );

        return Configuration[node]
               ?? throw new InvalidOperationException(
                   $"缺少 Cache KeyPrefix 配置[{node}]。"
               );
    }

    public string GetRedisConnectionString()
    {
        var node = StringAssist.Join(
            [
                CacheConfigureNode,
                RedisConfigureNode,
                "ConnectionString"
            ],
            ":"
        );

        return Configuration[node]
               ?? throw new InvalidOperationException(
                   $"缺少 Cache Redis ConnectionString 配置[{node}]。"
               );
    }

    public string GetSentinelCollection()
    {
        var node = StringAssist.Join(
            [
                CacheConfigureNode,
                RedisConfigureNode,
                "Sentinel"
            ],
            ":"
        );

        return Configuration[node]
               ?? throw new InvalidOperationException(
                   $"缺少 Cache Redis Sentinel 配置[{node}]。"
               );
    }

    #endregion
}