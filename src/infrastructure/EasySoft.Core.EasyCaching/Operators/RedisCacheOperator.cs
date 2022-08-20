using EasyCaching.Core;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.Result;

namespace EasySoft.Core.EasyCaching.Operators;

public class RedisCacheOperator : BaseCacheOperator
{
    private readonly IRedisCachingProvider _provider;

    public RedisCacheOperator(IRedisCachingProvider provider)
    {
        _provider = provider;
    }

    public override ExecutiveResult<T> Get<T>(string key)
    {
        var value = _provider.StringGet(key);

        return ConvertAssist.StringTo<T>(value);
    }

    private string ValueToString<T>(T value)
    {
        if (value == null)
        {
            throw new Exception("do not set null to cache");
        }

        string valueAdjust;

        var typeCode = Type.GetTypeCode(typeof(T));

        switch (typeCode)
        {
            case TypeCode.Object:
                valueAdjust = JsonConvertAssist.Serialize(value);
                break;

            default:
                valueAdjust = Convert.ToString(value) ?? "";
                break;
        }

        return valueAdjust;
    }

    public override void Set<T>(string key, T value, TimeSpan expiration)
    {
        _provider.StringSet(key, ValueToString(value), expiration);
    }

    public override void Remove(string key)
    {
        _provider.KeyDel(key);
    }

    public override void RemoveBatch(IEnumerable<string> keys)
    {
        foreach (var key in keys)
        {
            _provider.KeyDel(key);
        }
    }

    public override void RemoveByPrefix(string prefix)
    {
        var keys = _provider.SearchKeys(prefix);

        RemoveBatch(keys);
    }

    #region async

    public override async Task<ExecutiveResult<T>> GetAsync<T>(string key)
    {
        var value = await _provider.StringGetAsync(key);

        return ConvertAssist.StringTo<T>(value);
    }

    public override async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        await _provider.StringSetAsync(key, ValueToString(value), expiration);
    }

    public override async Task RemoveAsync(string key)
    {
        await _provider.KeyDelAsync(key);
    }

    public override async Task RemoveBatchAsync(IEnumerable<string> keys)
    {
        foreach (var key in keys)
        {
            await RemoveAsync(key);
        }
    }

    public override async Task RemoveByPrefixAsync(string prefix)
    {
        var keys = await _provider.SearchKeysAsync(prefix);

        await RemoveBatchAsync(keys);
    }

    #endregion
}