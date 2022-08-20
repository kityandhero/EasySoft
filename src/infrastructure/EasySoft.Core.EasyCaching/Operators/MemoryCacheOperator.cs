using EasyCaching.Core;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.Result;

namespace EasySoft.Core.EasyCaching.Operators;

public class MemoryCacheOperator : BaseCacheOperator
{
    private readonly IEasyCachingProvider _provider;

    public MemoryCacheOperator(IEasyCachingProvider provider)
    {
        _provider = provider;
    }

    public override ExecutiveResult<T> Get<T>(string key)
    {
        var c = _provider.Get<T>(key);

        if (c == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = c.Value
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

    public override async Task<ExecutiveResult<T>> GetAsync<T>(string key)
    {
        var c = await _provider.GetAsync<T>(key);

        if (c == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = c.Value
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