using EasySoft.Core.EasyCaching.Operators.Bases;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Implements;
using Newtonsoft.Json;

namespace EasySoft.Core.EasyCaching.Operators;

public abstract class GeneralCacheOperator : BaseCacheOperator
{
    private readonly IEasyCachingProvider _provider;

    protected GeneralCacheOperator(IEasyCachingProvider provider)
    {
        _provider = provider;
    }

    protected override ExecutiveResult<string> GetSerializedValue(string key)
    {
        var cacheValue = _provider.Get<string>(key);

        if (!cacheValue.HasValue)
        {
            return new ExecutiveResult<string>(ReturnCode.NoData);
        }

        return new ExecutiveResult<string>(ReturnCode.Ok)
        {
            Data = cacheValue.Value
        };
    }

    public override void Set<T>(string key, T value, TimeSpan expiration)
    {
        _provider.Set(
            key,
            JsonConvert.SerializeObject(value),
            expiration
        );
    }

    public override bool Remove(string key)
    {
        _provider.Remove(key);

        return true;
    }

    public override bool RemoveBatch(IEnumerable<string> keys)
    {
        _provider.RemoveAll(keys);

        return true;
    }

    public override bool RemoveByPrefix(string prefix)
    {
        _provider.RemoveByPrefix(prefix);

        return true;
    }

    #region async

    protected override async Task<ExecutiveResult<T>> GetCoreAsync<T>(string key)
    {
        var cacheValue = await _provider.GetAsync<T>(key);

        if (!cacheValue.HasValue)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = cacheValue.Value
        };
    }

    public override async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        await _provider.SetAsync(
            key,
            value,
            expiration
        );
    }

    public override async Task RemoveAsync(string key)
    {
        await _provider.RemoveAsync(key);
    }

    public override async Task RemoveBatchAsync(IEnumerable<string> keys)
    {
        await _provider.RemoveAllAsync(keys);
    }

    public override async Task RemoveByPrefixAsync(string prefix)
    {
        await _provider.RemoveByPrefixAsync(prefix);
    }

    #endregion
}