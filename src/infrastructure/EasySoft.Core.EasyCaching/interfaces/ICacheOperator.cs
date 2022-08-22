using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.EasyCaching.interfaces;

public interface ICacheOperator
{
    public string BuildKey(params string[] nameArray);

    public ExecutiveResult<T> Get<T>(string key);

    public void Set<T>(string key, T value, TimeSpan expiration);

    public void Set<T>(string key, T value, DateTime dateTime);

    public void Set<T>(string key, T value, DateTime dateTimeMin, DateTime dateTimeMax);

    public void Set<T>(string key, T value, int expirationRatio, int secondMin, int secondMax);

    public void Set<T>(string key, T value, int expirationRatio, TimeSpan expirationMin, TimeSpan expirationMax);

    public void Set<T>(
        string key,
        T value,
        DateTime moment,
        int expirationRatio,
        TimeSpan expirationMin,
        TimeSpan expirationMax
    );

    public void Remove(string key);

    public void RemoveBatch(IEnumerable<string> keys);

    public void RemoveByPrefix(string prefix);

    #region async

    public Task<ExecutiveResult<T>> GetAsync<T>(string key);

    public Task SetAsync<T>(string key, T value, TimeSpan expiration);

    public Task SetAsync<T>(string key, T value, DateTime dateTime);

    public Task SetAsync<T>(string key, T value, DateTime dateTimeMin, DateTime dateTimeMax);

    public Task SetAsync<T>(string key, T value, int expirationRatio, int secondMin, int secondMax);

    public Task SetAsync<T>(
        string key,
        T value,
        int expirationRatio,
        TimeSpan expirationMin,
        TimeSpan expirationMax
    );

    public Task SetAsync<T>(
        string key,
        T value,
        DateTime moment,
        int expirationRatio,
        TimeSpan expirationMin,
        TimeSpan expirationMax
    );

    public Task RemoveAsync(string key);

    public Task RemoveBatchAsync(IEnumerable<string> keys);

    public Task RemoveByPrefixAsync(string prefix);

    #endregion
}