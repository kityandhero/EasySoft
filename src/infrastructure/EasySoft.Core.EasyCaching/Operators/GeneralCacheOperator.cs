using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.EasyCaching.Operators;

public abstract class GeneralCacheOperator : BaseCacheOperator
{
    private readonly IEasyCachingProvider _provider;

    public GeneralCacheOperator(IEasyCachingProvider provider)
    {
        _provider = provider;
    }

    protected override ExecutiveResult<T> GetCore<T>(string key)
    {
        var cacheValue = _provider.Get<T>(key);

        if (!cacheValue.HasValue) return new ExecutiveResult<T>(ReturnCode.NoData);

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = cacheValue.Value
        };
    }

    public override void Set<T>(string key, T value, TimeSpan expiration)
    {
        _provider.Set(key, value, expiration);
    }

    public override void Remove(string key)
    {
        _provider.Remove(key);
    }

    public override void RemoveBatch(IEnumerable<string> keys)
    {
        _provider.RemoveAll(keys);
    }

    public override void RemoveByPrefix(string prefix)
    {
        _provider.RemoveByPrefix(prefix);
    }

    #region async

    protected override async Task<ExecutiveResult<T>> GetCoreAsync<T>(string key)
    {
        var cacheValue = await _provider.GetAsync<T>(key);

        if (!cacheValue.HasValue) return new ExecutiveResult<T>(ReturnCode.NoData);

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = cacheValue.Value
        };
    }

    public override async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        await _provider.SetAsync(key, value, expiration);
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