using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Core.EasyCaching.Operators.Bases;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.EasyCaching.Operators;

public class RedisFeatureCacheOperator : BaseCacheOperator, IRedisFeatureCacheOperator
{
    private readonly IRedisCachingProvider _provider;

    public RedisFeatureCacheOperator(IRedisCachingProvider provider)
    {
        _provider = provider;
    }

    protected override ExecutiveResult<string> GetSerializedValue(string key)
    {
        var value = _provider.StringGet(key);

        return string.IsNullOrEmpty(value)
            ? new ExecutiveResult<string>(ReturnCode.NoData)
            : ConvertAssist.StringTo<string>(value);
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
                valueAdjust = JsonConvertAssist.SerializeObject(value);
                break;

            default:
                valueAdjust = Convert.ToString(value) ?? "";
                break;
        }

        return valueAdjust;
    }

    public override void Set<T>(string key, T value, TimeSpan expiration)
    {
        _provider.StringSet(
            key,
            ValueToString(value),
            expiration
        );
    }

    public override bool Remove(string key)
    {
        _provider.KeyDel(key);

        return true;
    }

    public override bool RemoveBatch(IEnumerable<string> keys)
    {
        foreach (var key in keys)
        {
            _provider.KeyDel(key);
        }

        return true;
    }

    public override bool RemoveByPrefix(string prefix)
    {
        var keys = _provider.SearchKeys(prefix);

        RemoveBatch(keys);

        return true;
    }

    #region async

    protected override async Task<ExecutiveResult<T>> GetCoreAsync<T>(string key)
    {
        var value = await _provider.StringGetAsync(key);

        return string.IsNullOrEmpty(value)
            ? new ExecutiveResult<T>(ReturnCode.NoData)
            : ConvertAssist.StringTo<T>(value);
    }

    public override async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        await _provider.StringSetAsync(
            key,
            ValueToString(value),
            expiration
        );
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