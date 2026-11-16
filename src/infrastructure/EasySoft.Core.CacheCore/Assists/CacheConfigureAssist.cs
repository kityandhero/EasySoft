using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Models;

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

    /// <summary>
    /// 缓存配置
    /// </summary>
    /// <param name="configuration"></param>
    public CacheConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetCacheMode

    /// <summary>
    /// 缓存模式
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetCacheMode(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                CacheConfigureNode,
                CacheModeConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "inMemory";

        if (!v.In("inMemory", "redis"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，请设置 inMemory|redis"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 缓存模式
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetCacheMode()
    {
        return GetCacheMode(Configuration);
    }

    #endregion

    #region GetCacheKeyPrefix

    /// <summary>
    /// 缓存前缀
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetCacheKeyPrefix(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                CacheConfigureNode,
                CacheKeyPrefixConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 缓存前缀
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetCacheKeyPrefix()
    {
        return GetCacheKeyPrefix(Configuration);
    }

    #endregion

    #region GetRedisConnectionString

    /// <summary>
    /// GetRedisConnectionString
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetRedisConnectionString(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                CacheConfigureNode,
                RedisConfigureNode,
                "ConnectionString"
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，请设置"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// GetRedisConnectionString
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetRedisConnectionString()
    {
        return GetRedisConnectionString(Configuration);
    }

    #endregion

    #region GetSentinelCollection

    /// <summary>
    /// GetSentinelCollection
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetSentinelCollection(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                CacheConfigureNode,
                RedisConfigureNode,
                "Sentinel"
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// GetSentinelCollection
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetSentinelCollection()
    {
        return GetSentinelCollection(Configuration);
    }

    #endregion

    #endregion
}