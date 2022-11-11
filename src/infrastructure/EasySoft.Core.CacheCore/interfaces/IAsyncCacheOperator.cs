namespace EasySoft.Core.CacheCore.interfaces;

public interface IAsyncCacheOperator : ICacheOperator
{
    #region async

    public Task<ExecutiveResult<T>> GetAsync<T>(string key);

    /// <summary>
    /// 滑动过期模式
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="initialTime">初始过期时间</param>
    /// <param name="slidingTime">每次的滑动过期时间</param>
    /// <typeparam name="T"></typeparam>
    public Task SetAsync<T>(string key, T value, TimeSpan initialTime, TimeSpan slidingTime);

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