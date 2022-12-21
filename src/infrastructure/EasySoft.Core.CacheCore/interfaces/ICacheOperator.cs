using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.CacheCore.interfaces;

public interface ICacheOperator
{
    public string BuildKey(params string[] nameArray);

    public ExecutiveResult<T> Get<T>(string key);

    /// <summary>
    /// 滑动过期模式
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="initialTime">初始过期时间</param>
    /// <param name="slidingTime">每次的滑动过期时间</param>
    /// <typeparam name="T"></typeparam>
    public void Set<T>(string key, T value, TimeSpan initialTime, TimeSpan slidingTime);

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
}