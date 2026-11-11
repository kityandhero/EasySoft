using System.Collections;
using System.Text.RegularExpressions;
using EasySoft.Core.CacheCore.Assists;
using EasySoft.Core.CacheImplementation.Operators.Bases;
using EasySoft.Core.CacheImplementation.Providers;
using EasySoft.UtilityTools.Standard.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace EasySoft.Core.CacheImplementation.Operators;

public class MemoryCacheOperator : BaseCacheOperator
{
    public MemoryCacheOperator(
        CacheConfigureAssist cacheConfigureAssist,
        CacheKeyAssist cacheKeyAssist
    ) : base(cacheConfigureAssist, cacheKeyAssist)
    {
    }

    #region HasKey

    /// <inheritdoc />
    internal override bool HasKeyCore(string key)
    {
        var cache = MemoryCacheProvider.GetInstance();

        var exist = cache.TryGetValue(key, out _);

        return exist;
    }

    #endregion

    #region Increment

    /// <inheritdoc />
    internal override long StringIncrementCore(string key, int count)
    {
        throw new UnhandledException("in memory cache do not support StringIncrement");
    }

    #endregion

    #region Get

    /// <inheritdoc />
    internal override string StringGetCore(string key)
    {
        var cache = MemoryCacheProvider.GetInstance();

        var v = cache.Get<string>(key);

        return string.IsNullOrWhiteSpace(v) ? "" : v;
    }

    #endregion

    #region Set

    /// <inheritdoc />
    internal override bool SetValueCore<T>(string key, T value, TimeSpan expireTime)
    {
        var cache = MemoryCacheProvider.GetInstance();

        if (value is string v)
        {
            cache.Set(
                key,
                v,
                expireTime
            );
        }
        else
        {
            v = value.IsDecimal() ? value.ToString() : JsonConvertAssist.SerializeObject(value);

            cache.Set(
                key,
                v,
                expireTime
            );
        }

        return true;
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