using EasySoft.UtilityTools.Standard.Result.Implements;

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
    public Task SetAsync<T>(
        string key,
        T value,
        TimeSpan initialTime,
        TimeSpan slidingTime
    );

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiration"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task SetAsync<T>(string key, T value, TimeSpan expiration);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="dateTime"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task SetAsync<T>(string key, T value, DateTime dateTime);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="dateTimeMin"></param>
    /// <param name="dateTimeMax"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task SetAsync<T>(
        string key,
        T value,
        DateTime dateTimeMin,
        DateTime dateTimeMax
    );

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expirationRatio"></param>
    /// <param name="secondMin"></param>
    /// <param name="secondMax"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task SetAsync<T>(
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
    public Task SetAsync<T>(
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
    public Task SetAsync<T>(
        string key,
        T value,
        DateTime moment,
        int expirationRatio,
        TimeSpan expirationMin,
        TimeSpan expirationMax
    );

    /// <summary>
    /// 移除指定缓存
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Task RemoveAsync(string key);

    #endregion
}