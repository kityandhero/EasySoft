using System.Collections;
using System.Text.RegularExpressions;
using EasySoft.Core.CacheCore.Assists;
using EasySoft.Core.CacheImplementation.Operators.Bases;
using EasySoft.Core.CacheImplementation.Providers;

namespace EasySoft.Core.CacheImplementation.Operators;

public class RedisCacheOperator : BaseCacheOperator
{
    public RedisCacheOperator(
        CacheConfigureAssist cacheConfigureAssist,
        CacheKeyAssist cacheKeyAssist
    ) : base(cacheConfigureAssist, cacheKeyAssist)
    {
    }

    #region HasKey

    /// <inheritdoc />
    internal override bool HasKeyCore(string key)
    {
        var db = RedisCacheProvider.GetDatabase(CacheConfigureAssist.GetRedisConnectionString());

        return db.KeyExists(key);
    }

    #endregion

    #region Increment

    /// <inheritdoc />
    internal override long StringIncrementCore(string key, int count)
    {
        var db = RedisCacheProvider.GetDatabase(CacheConfigureAssist.GetRedisConnectionString());

        return db.StringIncrement(key, count);
    }

    #endregion

    #region Get

    /// <inheritdoc />
    internal override string StringGetCore(string key)
    {
        var result = "";

        var db = RedisCacheProvider.GetDatabase(CacheConfigureAssist.GetRedisConnectionString());

        var v = db.StringGet(key);

        if (!v.IsNullOrEmpty)
        {
            result = ConvertAssist.ObjectTo<string>(v);
        }

        return result;
    }

    #endregion

    #region Set

    /// <inheritdoc />
    internal override bool SetValueCore<T>(string key, T value, TimeSpan expireTime)
    {
        var db = RedisCacheProvider.GetDatabase(CacheConfigureAssist.GetRedisConnectionString());

        if (value is string v)
        {
            return db.StringSet(
                key,
                v,
                expireTime
            );
        }

        v = value.IsDecimal() ? value.ToString() : JsonConvert.SerializeObject(value);

        var result = db.StringSet(
            key,
            v,
            expireTime
        );

        return result;
    }

    #endregion

    #region Remove

    /// <inheritdoc />
    internal override bool RemoveCore(string key)
    {
        var cache = MemoryCacheProvider.GetInstance();

        cache.Remove(key);

        return true;
    }

    /// <inheritdoc />
    internal override bool RemoveBatchCore(string keyPattern)
    {
        if (string.IsNullOrWhiteSpace(keyPattern))
        {
            return false;
        }

        var cache = MemoryCacheProvider.GetInstance();

        //以下反射发现所有内存缓存，一旦量大了，或者在分布式下，就不能去枚举，性能会严重下降

        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

        var coherentState = cache.GetType().GetField("_coherentState", flags)?.GetValue(cache);
        var entries = coherentState?.GetType().GetField("_entries", flags)?.GetValue(coherentState);

        if (entries is IDictionary cacheItems)
        {
            foreach (DictionaryEntry cacheItem in cacheItems)
            {
                var contain = $"^{keyPattern.Replace("*", "(.)*")}";

                var regex = new Regex(contain);

                if (cacheItem.Key == null)
                {
                    continue;
                }

                var key = cacheItem.Key.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                if (regex.IsMatch(key))
                {
                    cache.Remove(key);
                }
            }
        }

        return true;
    }

    #endregion
}