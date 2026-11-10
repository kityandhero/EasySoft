using EasySoft.Core.CacheImplementation.CacheFactories;
using EasySoft.Core.CacheImplementation.Operators.Bases;

namespace EasySoft.Core.CacheImplementation.Operators;

public class GeneralCacheOperator : BaseCacheOperator
{
    private readonly IFusionCache _cacheExecutor;

    public GeneralCacheOperator(
        IFusionCacheFactory cacheFactory,
        string globalPrefix
    ) : base(globalPrefix)
    {
        _cacheExecutor = cacheFactory.GetCache(ConstCollection.FusionCacheName);
    }

    #region Get

    protected override T GetCore<T>(string key)
    {
        return _cacheExecutor.GetOrDefault<T>(key);
    }

    protected override async Task<T> GetAsyncCore<T>(string key)
    {
        return await _cacheExecutor.GetOrDefaultAsync<T>(key);
    }

    // protected override async Task<ExecutiveResult<T>> GetCoreAsync<T>(string key)
    // {
    //     var cacheValue = await _cacheExecutor.GetOrDefaultAsync<T>(key);
    //
    //     if (cacheValue == null)
    //     {
    //         return new ExecutiveResult<T>(ReturnCode.NoData);
    //     }
    //
    //     return new ExecutiveResult<T>(ReturnCode.Ok)
    //     {
    //         Data = cacheValue
    //     };
    // }

    // public override ExecutiveResult<string> GetSerializedValue(string key)
    // {
    //     var cacheValue = _cacheExecutor.GetOrDefault<string>(key);
    //
    //     if (cacheValue == null)
    //     {
    //         return new ExecutiveResult<string>(ReturnCode.Ok)
    //         {
    //             Data = ""
    //         };
    //     }
    //
    //     return new ExecutiveResult<string>(ReturnCode.Ok)
    //     {
    //         Data = cacheValue
    //     };
    // }

    #endregion

    #region Set

    protected override async Task SetAsyncCore<T>(string key, T value, TimeSpan expiration)
    {
        await _cacheExecutor.SetAsync(
            key,
            value,
            expiration
        );
    }

    protected override void SetCore<T>(string key, T value, TimeSpan expiration)
    {
        _cacheExecutor.Set(
            key,
            value,
            expiration
        );
    }

    #endregion

    #region Remove

    public override bool Remove(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return true;
        }

        _cacheExecutor.Remove(key);

        return true;
    }

    public override bool RemoveBatch(IEnumerable<string> keys)
    {
        foreach (var key in keys)
        {
            Remove(key);
        }

        return true;
    }

    public override async Task RemoveAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return;
        }

        await _cacheExecutor.RemoveAsync(key);
    }

    public override async Task RemoveBatchAsync(IEnumerable<string> keys)
    {
        foreach (var key in keys)
        {
            await RemoveAsync(key);
        }
    }

    #endregion
}