using EasySoft.Core.CacheImplementation.Entities;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.CacheImplementation.Operators.Bases;

public abstract partial class BaseCacheOperator : IAsyncCacheOperator
{
    #region Properties

    public string GlobalPrefix { get; private set; }

    #endregion

    protected BaseCacheOperator(string globalPrefix)
    {
        GlobalPrefix = globalPrefix;
    }

    public virtual string GetKeyPrefix()
    {
        return GeneralConfigAssist.GetKeyPrefix();
    }

    #region ExpireTime

    public TimeSpan GetFiveMinuteExpireExpireTime()
    {
        return TimeSpan.FromTicks(TimeSpan.TicksPerMinute * 5);
    }

    public TimeSpan GetSuperiorCacheExpireTime()
    {
        var totalSeconds = RandomEx.ThreadSafeNext(20, 30);

        return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
    }

    public TimeSpan GetProvisionalCacheExpireTime()
    {
        var now = DateTime.Now;

        //过期时间限制为凌晨的2点~5:59点之间
        var totalSeconds = (long)now.AddHours(24 - now.Hour + 1).Subtract(now).TotalSeconds +
                           RandomEx.ThreadSafeNext(3600, 28800);

        return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
    }

    public TimeSpan GetSteadyCacheExpireTime()
    {
        // （60 ~ 120 天）
        var totalSeconds = 12 * RandomEx.ThreadSafeNext(3600, 7200) * 120;

        return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
    }

    #endregion

    #region BuildKey

    public string BuildKey<T>(params string[] nameArray)
    {
        var nameArrayAdjust = new List<string>
        {
            typeof(T).Name.ToLowerFirst()
        };

        nameArrayAdjust.AddRange(nameArray);

        return BuildKey(nameArrayAdjust.ToArray());
    }

    public string BuildKey(params string[] nameArray)
    {
        if (nameArray == null)
        {
            throw new Exception("参数不能为null");
        }

        if (nameArray.Length == 0)
        {
            throw new Exception("不能构建空的缓存键名");
        }

        var nameList = new List<string>
        {
            GlobalPrefix,
            nameArray
        };

        var result = nameList.Where(o => !string.IsNullOrWhiteSpace(o))
            .Select(o => o.Remove(" ").Trim().ToLowerFirst())
            .Join(":");

        if (string.IsNullOrWhiteSpace(result))
        {
            throw new Exception("构建缓存键名发生错误");
        }

        return result;
    }

    #endregion

    #region Get

    protected abstract T GetCore<T>(string key);

    /// <summary>
    /// 获取序列化的缓存值
    /// </summary>
    /// <param name="key">键名</param>
    /// <returns>序列化字符串</returns>
    private ExecutiveResult<string> GetCacheValue(string key)
    {
        var serializedValue = GetCore<string>(key);

        if (serializedValue == null)
        {
            return new ExecutiveResult<string>(ReturnCode.NoData);
        }

        return new ExecutiveResult<string>(ReturnCode.Ok)
        {
            Data = serializedValue
        };
    }

    public ExecutiveResult<T> Get<T>(string key)
    {
        var executiveResult = GetCacheValue(key);

        if (!executiveResult.Success)
        {
            return new ExecutiveResult<T>(executiveResult.Code);
        }

        if (executiveResult.Data == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        try
        {
            var slidingWrapper = JsonConvertAssist.DeserializeObject<SlidingWrapper<T>>(executiveResult.Data);

            var innerValue = slidingWrapper.Value;

            if (innerValue == null)
            {
                return new ExecutiveResult<T>(ReturnCode.NoData);
            }

            if (slidingWrapper.SlidingTime > TimeSpan.Zero)
            {
                Set(
                    key,
                    innerValue,
                    slidingWrapper.SlidingTime,
                    slidingWrapper.SlidingTime
                );
            }

            return new ExecutiveResult<T>(ReturnCode.Ok)
            {
                Data = slidingWrapper.Value
            };
        }
        catch (Exception)
        {
            try
            {
                var deserializeValue = JsonConvertAssist.DeserializeObject<T>(executiveResult.Data);

                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = deserializeValue
                };
            }
            catch (Exception e)
            {
                e.Data.Add("json", executiveResult.Data);

                throw;
            }
        }
    }

    public ExecutiveResult<string> GetSerializedValue<T>(string key)
    {
        var executiveResult = Get<T>(key);

        if (!executiveResult.Success)
        {
            return new ExecutiveResult<string>(executiveResult.Code);
        }

        return new ExecutiveResult<string>(ReturnCode.Ok)
        {
            Data = JsonConvertAssist.SerializeObject(executiveResult.Data)
        };
    }

    #endregion

    #region Get Async

    protected abstract Task<T> GetAsyncCore<T>(string key);

    /// <summary>
    /// 获取序列化的缓存值
    /// </summary>
    /// <param name="key">键名</param>
    /// <returns>序列化字符串</returns>
    private async Task<ExecutiveResult<string>> GetSerializedValueAsync(string key)
    {
        var serializedValue = await GetAsyncCore<string>(key);

        if (serializedValue == null)
        {
            return new ExecutiveResult<string>(ReturnCode.NoData);
        }

        return new ExecutiveResult<string>(ReturnCode.Ok)
        {
            Data = serializedValue
        };
    }

    public async Task<ExecutiveResult<T>> GetAsync<T>(string key)
    {
        var executiveResult = await GetSerializedValueAsync(key);

        if (!executiveResult.Success)
        {
            return new ExecutiveResult<T>(executiveResult.Code);
        }

        if (executiveResult.Data == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        try
        {
            var slidingWrapper = JsonConvertAssist.DeserializeObject<SlidingWrapper<T>>(executiveResult.Data);

            var innerValue = slidingWrapper.Value;

            if (innerValue == null)
            {
                return new ExecutiveResult<T>(ReturnCode.NoData);
            }

            if (slidingWrapper.SlidingTime > TimeSpan.Zero)
            {
                await SetAsync(
                    key,
                    innerValue,
                    slidingWrapper.SlidingTime,
                    slidingWrapper.SlidingTime
                );
            }

            return new ExecutiveResult<T>(ReturnCode.Ok)
            {
                Data = slidingWrapper.Value
            };
        }
        catch (Exception)
        {
            try
            {
                var deserializeValue = JsonConvertAssist.DeserializeObject<T>(executiveResult.Data);

                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = deserializeValue
                };
            }
            catch (Exception e)
            {
                e.Data.Add("json", executiveResult.Data);

                throw;
            }
        }
    }

    #endregion

    #region Set

    protected abstract void SetCore<T>(string key, T value, TimeSpan expiration);

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        var serializedValue = JsonConvertAssist.SerializeObject(value);

        SetCore(
            key,
            serializedValue,
            expiration
        );
    }

    public void Set<T>(
        string key,
        T value,
        TimeSpan initialTime,
        TimeSpan slidingTime
    )
    {
        if (initialTime <= TimeSpan.Zero)
        {
            throw new Exception("初始过期时间值无效");
        }

        if (slidingTime <= TimeSpan.Zero)
        {
            throw new Exception("初始化滑动时间值无效");
        }

        var wrapper = new SlidingWrapper<T>
        {
            Value = value,
            SlidingTime = slidingTime
        };

        Set(
            key,
            wrapper,
            initialTime
        );
    }

    public void Set<T>(string key, T value, DateTime dateTime)
    {
        var timeSpan = dateTime.Subtract(DateTime.Now);

        if (timeSpan.Milliseconds < 0)
        {
            throw new Exception("设定的过期时间早于当前时间");
        }

        Set(
            key,
            value,
            timeSpan
        );
    }

    public void Set<T>(
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

        Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    public void Set<T>(
        string key,
        T value,
        int expirationRatio,
        int secondMin,
        int secondMax
    )
    {
        Set(
            key,
            value,
            expirationRatio,
            new TimeSpan(TimeSpan.TicksPerSecond * secondMin),
            new TimeSpan(TimeSpan.TicksPerSecond * secondMax)
        );
    }

    public void Set<T>(
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

        Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    public void Set<T>(
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

        Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    #endregion

    #region Set Async

    protected abstract Task SetAsyncCore<T>(string key, T value, TimeSpan expiration);

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var serializedValue = JsonConvertAssist.SerializeObject(value);

        await SetAsyncCore(
            key,
            serializedValue,
            expiration
        );
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan initialTime,
        TimeSpan slidingTime
    )
    {
        if (initialTime <= TimeSpan.Zero)
        {
            throw new Exception("初始过期时间值无效");
        }

        if (slidingTime <= TimeSpan.Zero)
        {
            throw new Exception("初始化滑动时间值无效");
        }

        var wrapper = new SlidingWrapper<T>
        {
            Value = value,
            SlidingTime = slidingTime
        };

        await SetAsync(
            key,
            wrapper,
            initialTime
        );
    }

    public async Task SetAsync<T>(string key, T value, DateTime dateTime)
    {
        var timeSpan = dateTime.Subtract(DateTime.Now);

        if (timeSpan.Milliseconds < 0)
        {
            throw new Exception("设定的过期时间早于当前时间");
        }

        await SetAsync(
            key,
            value,
            timeSpan
        );
    }

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="dateTimeMin">区间最小值</param>
    /// <param name="dateTimeMax">区间最大值</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="Exception"></exception>
    public async Task SetAsync<T>(
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

        await SetAsync(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="expirationRatio">区间倍率</param>
    /// <param name="secondMin">秒区间最小值</param>
    /// <param name="secondMax">秒区间最大值</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="Exception"></exception>
    public async Task SetAsync<T>(
        string key,
        T value,
        int expirationRatio,
        int secondMin,
        int secondMax
    )
    {
        await SetAsync(
            key,
            value,
            expirationRatio,
            new TimeSpan(TimeSpan.TicksPerSecond * secondMin),
            new TimeSpan(TimeSpan.TicksPerSecond * secondMax)
        );
    }

    public async Task SetAsync<T>(
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

        await SetAsync(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    public async Task SetAsync<T>(
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

        await SetAsync(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds)
        );
    }

    #endregion

    #region Remove

    public abstract bool Remove(string key);

    public abstract bool RemoveBatch(IEnumerable<string> keys);

    #endregion

    #region RemoveAsync

    public abstract Task RemoveAsync(string key);

    public abstract Task RemoveBatchAsync(IEnumerable<string> keys);

    #endregion
}