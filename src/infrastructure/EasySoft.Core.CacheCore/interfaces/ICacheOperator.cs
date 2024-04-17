using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.CacheCore.interfaces;

public interface ICacheOperator : ITryGet
{
    /// <summary>
    /// 获取键名前缀  
    /// </summary>
    /// <returns></returns>
    public string GetKeyPrefix();

    #region ExpireTime

    /// <summary>
    /// 获取5分钟对应的缓存有效期
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetFiveMinuteExpireExpireTime();

    /// <summary>
    /// 获取临时性短暂的缓存过期时间 (20 ~ 30 秒)
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetSuperiorCacheExpireTime();

    /// <summary>
    /// 获取比较优异的缓存过期时间 （限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetProvisionalCacheExpireTime();

    /// <summary>
    /// 获取稳定的缓存过期时间（60 ~ 120 天）
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetSteadyCacheExpireTime();

    #endregion

    #region BuildKey

    public string BuildKey<T>(params string[] nameArray);

    public string BuildKey(params string[] nameArray);

    #endregion

    #region Get

    public ExecutiveResult<T> Get<T>(string key);

    #endregion

    #region Set

    /// <summary>
    /// 滑动过期模式
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="initialTime">初始过期时间</param>
    /// <param name="slidingTime">每次的滑动过期时间</param>
    /// <typeparam name="T"></typeparam>
    public void Set<T>(
        string key,
        T value,
        TimeSpan initialTime,
        TimeSpan slidingTime
    );

    /// <summary>
    /// 滑动过期模式
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expiration">过期时间</param>
    /// <typeparam name="T"></typeparam>
    public void Set<T>(
        string key,
        T value,
        TimeSpan expiration
    );

    /// <summary>
    /// 滑动过期模式
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="dateTime">过期时间</param>
    /// <typeparam name="T">T</typeparam>
    public void Set<T>(
        string key,
        T value,
        DateTime dateTime
    );

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="dateTimeMin">区间最小值</param>
    /// <param name="dateTimeMax">区间最大值</param>
    public void Set<T>(
        string key,
        T value,
        DateTime dateTimeMin,
        DateTime dateTimeMax
    );

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="expirationRatio">区间倍率</param>
    /// <param name="secondMin">秒区间最小值</param>
    /// <param name="secondMax">秒区间最大值</param>
    public void Set<T>(
        string key,
        T value,
        int expirationRatio,
        int secondMin,
        int secondMax
    );

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="expirationRatio">区间倍率</param>
    /// <param name="expirationMin">区间最小值</param>
    /// <param name="expirationMax">区间最大值</param>
    public void Set<T>(
        string key,
        T value,
        int expirationRatio,
        TimeSpan expirationMin,
        TimeSpan expirationMax
    );

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="moment">区间开始的某一时刻</param>
    /// <param name="expirationRatio">区间倍率</param>
    /// <param name="expirationMin">区间最小值</param>
    /// <param name="expirationMax">区间最大值</param>
    public void Set<T>(
        string key,
        T value,
        DateTime moment,
        int expirationRatio,
        TimeSpan expirationMin,
        TimeSpan expirationMax
    );

    #endregion

    #region Remove

    public bool Remove(string key);

    public bool RemoveBatch(IEnumerable<string> keys);

    public bool RemoveByPrefix(string prefix);

    #endregion
}