using EasySoft.Core.CacheCore.Assists;
using EasySoft.Core.CacheImplementation.Operators.Bases;

namespace EasySoft.Core.CacheImplementation.Operators;

public class GeneralCacheOperator : BaseCacheOperator
{
    #region Properties

    public MemoryCacheOperator MemoryCacheOperator { get; private set; }

    public RedisCacheOperator RedisCacheOperator { get; private set; }

    #endregion

    public GeneralCacheOperator(
        CacheConfigureAssist cacheConfigureAssist,
        CacheKeyAssist cacheKeyAssist,
        MemoryCacheOperator memoryCacheOperator,
        RedisCacheOperator redisCacheOperator
    ) : base(cacheConfigureAssist, cacheKeyAssist)
    {
        MemoryCacheOperator = memoryCacheOperator;
        RedisCacheOperator = redisCacheOperator;
    }

    #region HasKey

    /// <inheritdoc />
    internal override bool HasKeyCore(string key)
    {
        var currentCacheMode = CacheConfigureAssist.GetCacheMode();

        if (currentCacheMode == "redis")
        {
            return RedisCacheOperator.HasKeyCore(key);
        }

        return MemoryCacheOperator.HasKeyCore(key);
    }

    #endregion

    #region StringIncrement

    /// <inheritdoc />
    internal override long StringIncrementCore(string key, int count)
    {
        var currentCacheMode = CacheConfigureAssist.GetCacheMode();

        if (currentCacheMode == "redis")
        {
            return RedisCacheOperator.StringIncrementCore(key, count);
        }

        return MemoryCacheOperator.StringIncrementCore(key, count);
    }

    #endregion

    #region Get

    /// <inheritdoc />
    internal override string StringGetCore(string key)
    {
        var currentCacheMode = CacheConfigureAssist.GetCacheMode();

        if (currentCacheMode == "redis")
        {
            return RedisCacheOperator.StringGetCore(key);
        }

        return MemoryCacheOperator.StringGetCore(key);
    }

    #endregion

    #region Set

    /// <inheritdoc />
    internal override bool SetValueCore<T>(string key, T value, TimeSpan expireTime)
    {
        var currentCacheMode = CacheConfigureAssist.GetCacheMode();

        if (currentCacheMode == "redis")
        {
            return RedisCacheOperator.SetValueCore(
                key,
                value,
                expireTime
            );
        }

        return MemoryCacheOperator.SetValueCore(
            key,
            value,
            expireTime
        );
    }

    #endregion

    #region Remove

    /// <inheritdoc />
    internal override bool RemoveCore(string key)
    {
        var currentCacheMode = CacheConfigureAssist.GetCacheMode();

        if (currentCacheMode == "redis")
        {
            return RedisCacheOperator.RemoveCore(key);
        }

        return MemoryCacheOperator.RemoveCore(key);
    }

    /// <inheritdoc />
    internal override bool RemoveBatchCore(string key)
    {
        var currentCacheMode = CacheConfigureAssist.GetCacheMode();

        if (currentCacheMode == "redis")
        {
            return RedisCacheOperator.RemoveBatchCore(key);
        }

        return MemoryCacheOperator.RemoveBatchCore(key);
    }

    #endregion
}