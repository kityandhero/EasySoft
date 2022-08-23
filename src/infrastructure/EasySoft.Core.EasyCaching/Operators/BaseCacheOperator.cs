using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.Entities;
using EasySoft.UtilityTools.Standard;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.EasyCaching.Operators;

public abstract class BaseCacheOperator : IAsyncCacheOperator
{
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
            RedisConfigAssist.GetKeyPrefix().ToLowerFirst(),
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

    protected abstract ExecutiveResult<T> GetCore<T>(string key);

    public ExecutiveResult<T> Get<T>(string key)
    {
        var result = GetCore<object>(key);

        if (!result.Success)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        if (result.Data == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        var valueType = result.Data.GetType();

        if (valueType.IsGenericType &&
            valueType.GetGenericTypeDefinition() == typeof(SlidingWrapper<>))
        {
            if (result.Data is not SlidingWrapper<T> slidingWrapper)
            {
                return new ExecutiveResult<T>(ReturnCode.NoData);
            }

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

        if (result.Data is T transferData)
        {
            return new ExecutiveResult<T>(ReturnCode.Ok)
            {
                Data = transferData
            };
        }

        return new ExecutiveResult<T>(ReturnCode.NoData);
    }

    public void Set<T>(string key, T value, TimeSpan initialTime, TimeSpan slidingTime)
    {
        if (initialTime <= TimeSpan.Zero)
        {
            throw new Exception("初始过期时间值无效");
        }

        if (slidingTime <= TimeSpan.Zero)
        {
            throw new Exception("初始化滑动时间值无效");
        }

        var wrapper = new SlidingWrapper<T>()
        {
            Value = value,
            SlidingTime = slidingTime
        };

        Set(key, wrapper, initialTime);
    }

    public abstract void Set<T>(string key, T value, TimeSpan expiration);

    public void Set<T>(string key, T value, DateTime dateTime)
    {
        var timeSpan = dateTime.Subtract(DateTime.Now);

        if (timeSpan.Milliseconds < 0)
        {
            throw new Exception("设定的过期时间早于当前时间");
        }

        Set(key, value, timeSpan);
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
    public void Set<T>(string key, T value, DateTime dateTimeMin, DateTime dateTimeMax)
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

        Set(key, value, new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds));
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
    public void Set<T>(string key, T value, int expirationRatio, int secondMin, int secondMax)
    {
        Set(
            key,
            value,
            expirationRatio,
            new TimeSpan(TimeSpan.TicksPerSecond * secondMin),
            new TimeSpan(TimeSpan.TicksPerSecond * secondMax)
        );
    }

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="expirationRatio">区间倍率</param>
    /// <param name="expirationMin">区间最小值</param>
    /// <param name="expirationMax">区间最大值</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="Exception"></exception>
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

        Set(key, value, new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds));
    }

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="moment">区间开始的某一时刻</param>
    /// <param name="expirationRatio">区间倍率</param>
    /// <param name="expirationMin">区间最小值</param>
    /// <param name="expirationMax">区间最大值</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="Exception"></exception>
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

        Set(key, value, new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds));
    }

    public abstract void Remove(string key);

    public abstract void RemoveBatch(IEnumerable<string> keys);

    public abstract void RemoveByPrefix(string prefix);

    protected abstract Task<ExecutiveResult<T>> GetCoreAsync<T>(string key);

    public async Task<ExecutiveResult<T>> GetAsync<T>(string key)
    {
        var result = await GetCoreAsync<object>(key);

        if (!result.Success)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        if (result.Data == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        var valueType = result.Data.GetType();

        if (valueType.IsGenericType &&
            valueType.GetGenericTypeDefinition() == typeof(SlidingWrapper<>))
        {
            if (result.Data is not SlidingWrapper<T> slidingWrapper)
            {
                return new ExecutiveResult<T>(ReturnCode.NoData);
            }

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

        if (result.Data is T transferData)
        {
            return new ExecutiveResult<T>(ReturnCode.Ok)
            {
                Data = transferData
            };
        }

        return new ExecutiveResult<T>(ReturnCode.NoData);
    }

    public abstract Task SetAsync<T>(string key, T value, TimeSpan expiration);

    public async Task SetAsync<T>(string key, T value, TimeSpan initialTime, TimeSpan slidingTime)
    {
        if (initialTime <= TimeSpan.Zero)
        {
            throw new Exception("初始过期时间值无效");
        }

        if (slidingTime <= TimeSpan.Zero)
        {
            throw new Exception("初始化滑动时间值无效");
        }

        var wrapper = new SlidingWrapper<T>()
        {
            Value = value,
            SlidingTime = slidingTime
        };

        await SetAsync(key, wrapper, initialTime);
    }

    public async Task SetAsync<T>(string key, T value, DateTime dateTime)
    {
        var timeSpan = dateTime.Subtract(DateTime.Now);

        if (timeSpan.Milliseconds < 0)
        {
            throw new Exception("设定的过期时间早于当前时间");
        }

        await SetAsync(key, value, timeSpan);
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
    public async Task SetAsync<T>(string key, T value, DateTime dateTimeMin, DateTime dateTimeMax)
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

        await SetAsync(key, value, new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds));
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
    public async Task SetAsync<T>(string key, T value, int expirationRatio, int secondMin, int secondMax)
    {
        await SetAsync(
            key,
            value,
            expirationRatio,
            new TimeSpan(TimeSpan.TicksPerSecond * secondMin),
            new TimeSpan(TimeSpan.TicksPerSecond * secondMax)
        );
    }

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="expirationRatio">区间倍率</param>
    /// <param name="expirationMin">区间最小值</param>
    /// <param name="expirationMax">区间最大值</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="Exception"></exception>
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

        await SetAsync(key, value, new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds));
    }

    /// <summary>
    /// 设置随机时间范围的缓存,时间范围最大为 0 ~ int.MaxValue
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="moment">区间开始的某一时刻</param>
    /// <param name="expirationRatio">区间倍率</param>
    /// <param name="expirationMin">区间最小值</param>
    /// <param name="expirationMax">区间最大值</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="Exception"></exception>
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

        await SetAsync(key, value, new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds));
    }

    public abstract Task RemoveAsync(string key);

    public abstract Task RemoveBatchAsync(IEnumerable<string> keys);

    public abstract Task RemoveByPrefixAsync(string key);
}