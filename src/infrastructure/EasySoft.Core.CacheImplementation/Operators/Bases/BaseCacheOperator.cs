using EasySoft.Core.CacheCore.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.CacheImplementation.Operators.Bases;

public abstract partial class BaseCacheOperator : ICacheOperator
{
    #region Properties

    public CacheConfigureAssist CacheConfigureAssist { get; private set; }

    public CacheKeyAssist CacheKeyAssist { get; private set; }

    #endregion

    protected BaseCacheOperator(
        CacheConfigureAssist cacheConfigureAssist,
        CacheKeyAssist cacheKeyAssist
    )
    {
        CacheConfigureAssist = cacheConfigureAssist;
        CacheKeyAssist = cacheKeyAssist;
    }

    public virtual string GetKeyPrefix()
    {
        return GeneralConfigAssist.GetKeyPrefix();
    }

    #region Has

    internal abstract bool HasKeyCore(string key);

    /// <inheritdoc />
    public bool HasKey(string key)
    {
        var keyAdjust = CacheKeyAssist.BuildKey(key);

        return HasKeyCore(keyAdjust);
    }

    #endregion

    #region Increment

    internal abstract long StringIncrementCore(string key, int count);

    /// <inheritdoc />
    public long StringIncrement(string key, int count)
    {
        var keyAdjust = CacheKeyAssist.BuildKey(key);

        return StringIncrementCore(keyAdjust, count);
    }

    #endregion

    #region Get

    internal abstract string StringGetCore(string key);

    /// <inheritdoc />
    public string StringGet(string key)
    {
        var keyAdjust = CacheKeyAssist.BuildKey(key);

        return StringGetCore(keyAdjust);
    }

    public ExecutiveResult<string> GetSerializedValue(string key)
    {
        return GetSerializedValue(
            key,
            ""
        );
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue">不存在时默认的值</param>
    /// <returns></returns>
    public ExecutiveResult<string> GetSerializedValue(string key, string defaultValue)
    {
        return GetSerializedValue(
            key,
            false,
            defaultValue,
            CacheTimeAssist.DefaultExpireTimeSpan
        );
    }

    /// <inheritdoc />
    public ExecutiveResult<string> GetSerializedValue(
        string key,
        bool setDefaultValueWhenNoExist,
        string defaultValue,
        TimeSpan expireTime
    )
    {
        var result = defaultValue;

        var exist = false;

        var v = StringGet(key);

        if (string.IsNullOrWhiteSpace(v))
        {
            exist = true;

            result = ConvertAssist.ObjectTo<string>(v);
        }

        if (exist)
        {
            return new ExecutiveResult<string>(ReturnCode.Ok)
            {
                Data = result
            };
        }

        if (!exist && setDefaultValueWhenNoExist)
        {
            SetValue(
                key,
                defaultValue,
                expireTime
            );

            return new ExecutiveResult<string>(ReturnCode.Ok)
            {
                Data = result
            };
        }

        return new ExecutiveResult<string>(ReturnCode.NoData);
    }

    #endregion

    #region Set

    internal abstract bool SetValueCore<T>(
        string key,
        T value,
        TimeSpan expiration
    );

    public bool SetValue<T>(
        string key,
        T value,
        TimeSpan expiration
    )
    {
        var keyAdjust = CacheKeyAssist.BuildKey(key);

        return SetValueCore(
            keyAdjust,
            value,
            expiration
        );
    }

    /// <inheritdoc />
    public string SaveWithGuidKey(string data)
    {
        return SaveWithGuidKey(data, new TimeSpan(TimeSpan.TicksPerSecond * 600));
    }

    /// <inheritdoc />
    public string SaveWithGuidKey(string data, TimeSpan expireTime)
    {
        var key = Guid.NewGuid().ToString().Remove("-").ToLower();

        SetValue(
            key,
            data,
            expireTime
        );

        return key;
    }

    /// <inheritdoc />
    public bool Set<T>(
        string key,
        T value,
        TimeSpan expiration
    )
    {
        var serializedValue = JsonConvertAssist.SerializeObject(value);

        return SetValue(
            key,
            serializedValue,
            expiration
        );
    }

    /// <inheritdoc />
    public bool Set<T>(
        string key,
        T value,
        DateTime dateTime
    )
    {
        var timeSpan = dateTime.Subtract(DateTime.Now);

        if (timeSpan.Milliseconds < 0)
        {
            throw new Exception("设定的过期时间早于当前时间");
        }

        return Set(
            key,
            value,
            timeSpan
        );
    }

    /// <inheritdoc />
    public bool Set<T>(
        string key,
        T value,
        DateTime dateTimeMin,
        DateTime dateTimeMax
    )
    {
        if (dateTimeMin < DateTime.Now)
        {
            throw new Exception("设定的最小区间值不能早于当前");
        }

        if (dateTimeMax < DateTime.Now)
        {
            throw new Exception("设定的最大区间值不能早于当前");
        }

        if (dateTimeMin < dateTimeMax)
        {
            throw new Exception("设定的随机时间间隔区间无效");
        }

        var totalSeconds = RandomEx.ThreadSafeNext(
            (int)dateTimeMin.Subtract(DateTime.Now).TotalSeconds,
            (int)dateTimeMax.Subtract(DateTime.Now).TotalSeconds
        );

        return Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    /// <inheritdoc />
    public bool Set<T>(
        string key,
        T value,
        int expirationRatio,
        int secondMin,
        int secondMax
    )
    {
        return Set(
            key,
            value,
            expirationRatio,
            new TimeSpan(TimeSpan.TicksPerSecond * secondMin),
            new TimeSpan(TimeSpan.TicksPerSecond * secondMax)
        );
    }

    /// <inheritdoc />
    public bool Set<T>(
        string key,
        T value,
        int expirationRatio,
        TimeSpan expirationMin,
        TimeSpan expirationMax
    )
    {
        if (expirationMin < expirationMax)
        {
            throw new Exception("设定的随机时间间隔区间无效");
        }

        if (expirationRatio <= 0)
        {
            throw new Exception("设定的区间倍率无效,不能效于1");
        }

        var totalSeconds = expirationRatio * RandomEx.ThreadSafeNext(
            (int)expirationMin.TotalSeconds,
            (int)expirationMax.TotalSeconds
        );

        return Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    /// <inheritdoc />
    public bool Set<T>(
        string key,
        T value,
        DateTime moment,
        int expirationRatio,
        TimeSpan expirationMin,
        TimeSpan expirationMax
    )
    {
        if (moment < DateTime.Now)
        {
            throw new Exception("指定的时刻不能早于当前");
        }

        if (expirationMin < expirationMax)
        {
            throw new Exception("设定的随机时间间隔区间无效");
        }

        if (expirationRatio <= 0)
        {
            throw new Exception("设定的区间倍率无效,不能效于1");
        }

        var totalSeconds = (int)moment.Subtract(DateTime.Now).TotalSeconds + expirationRatio * RandomEx.ThreadSafeNext(
            (int)expirationMin.TotalSeconds,
            (int)expirationMax.TotalSeconds
        );

        return Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    #endregion

    #region Remove

    internal abstract bool RemoveCore(string key);

    /// <inheritdoc />
    public bool Remove(string key)
    {
        var keyAdjust = CacheKeyAssist.BuildKey(key);

        return RemoveCore(keyAdjust);
    }

    /// <inheritdoc />
    public void RemoveBatch(IEnumerable<string> keys)
    {
        foreach (var key in keys)
        {
            Remove(key);
        }
    }

    internal abstract bool RemoveBatchCore(string keyPattern);

    /// <inheritdoc />
    public bool RemoveBatch(string keyPattern)
    {
        var keyAdjust = CacheKeyAssist.BuildKey(keyPattern);

        return RemoveBatchCore(keyAdjust);
    }

    #endregion
}