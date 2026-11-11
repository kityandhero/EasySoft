using EasySoft.Core.CacheCore.Assists;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.CacheCore.interfaces;

public interface ICacheOperator : ITryGet
{
    public CacheConfigureAssist CacheConfigureAssist { get; }

    public CacheKeyAssist CacheKeyAssist { get; }

    #region Has

    /// <summary>
    /// 是否存在键
    /// </summary>
    /// <param name="key">键名</param>
    /// <returns>检测结果</returns>
    public bool HasKey(string key);

    #endregion

    #region Increment

    public long StringIncrement(string key, int count);

    #endregion

    #region Get

    /// <summary>
    /// 获取序列化值
    /// </summary>
    /// <param name="key">键名</param>
    /// <returns>序列化值</returns>
    public ExecutiveResult<string> GetSerializedValue(string key);

    /// <summary>
    /// 获取序列化值
    /// </summary>
    /// <param name="key">键名</param>
    /// <param name="defaultValue">不存在时默认的值</param>
    /// <returns>序列化值</returns>
    public ExecutiveResult<string> GetSerializedValue(string key, string defaultValue);

    /// <summary>
    /// 获取序列化值
    /// </summary>
    /// <param name="key">键名</param>
    /// <param name="setDefaultValueWhenNoExist">不存在时时候设置默认值</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="expireTime">过期时间</param>
    /// <returns>序列化值</returns>
    public ExecutiveResult<string> GetSerializedValue(
        string key,
        bool setDefaultValueWhenNoExist,
        string defaultValue,
        TimeSpan expireTime
    );

    /// <summary>
    /// StringGet
    /// </summary>
    /// <param name="key">键名</param>
    /// <returns>值</returns>
    public string StringGet(string key);

    #endregion

    #region Set

    /// <summary>
    /// SaveWithGuidKey
    /// </summary>
    /// <param name="data">值</param>
    /// <returns>Guid</returns>
    public string SaveWithGuidKey(string data);

    /// <summary>
    /// SaveWithGuidKey
    /// </summary>
    /// <param name="data">值</param>
    /// <param name="expireTime">过期时间</param>
    /// <returns>Guid</returns>
    public string SaveWithGuidKey(string data, TimeSpan expireTime);

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expiration">过期时间</param>
    /// <typeparam name="T"></typeparam>
    public bool Set<T>(
        string key,
        T value,
        TimeSpan expiration
    );

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="dateTime">过期时间</param>
    /// <typeparam name="T">T</typeparam>
    public bool Set<T>(
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
    public bool Set<T>(
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
    public bool Set<T>(
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
    public bool Set<T>(
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
    public bool Set<T>(
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

    public void RemoveBatch(IEnumerable<string> keys);

    public bool RemoveBatch(string keyPattern);

    #endregion
}