using EasySoft.UtilityTools.Standard;

namespace EasySoft.Core.CacheCore.Assists;

public static class CacheTimeAssist
{
    public static readonly TimeSpan DefaultExpireTimeSpan = TimeSpan.FromTicks(TimeSpan.TicksPerMinute * 5);

    #region GetCacheExpireTime

    public static TimeSpan GetFiveMinuteExpireExpireTime()
    {
        return TimeSpan.FromTicks(TimeSpan.TicksPerMinute * 5);
    }

    /// <summary>
    /// 获取随机性短暂的缓存过期时间 (min ~ max 秒)
    /// </summary>
    /// <returns></returns>
    public static TimeSpan GetRandomCacheExpireTime(int min, int max)
    {
        if (min <= 0)
        {
            throw new Exception("min mast be getter than 0");
        }

        if (max <= min)
        {
            throw new Exception("max mast be getter than min");
        }

        var totalSeconds = RandomEx.ThreadSafeNext(min, max);

        return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
    }

    /// <summary>
    /// 获取临时性短暂的缓存过期时间 (20 ~ 30 秒)
    /// </summary>
    /// <returns></returns>
    public static TimeSpan GetSuperiorCacheExpireTime()
    {
        var totalSeconds = RandomEx.ThreadSafeNext(20, 30);

        return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
    }

    /// <summary>
    /// 获取比较优异的缓存过期时间 （限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <returns></returns>
    public static TimeSpan GetProvisionalCacheExpireTime()
    {
        var now = DateTime.Now;

        //过期时间限制为凌晨的2点~5:59点之间
        var totalSeconds = (long)now.AddHours(24 - now.Hour + 1).Subtract(now).TotalSeconds +
                           RandomEx.ThreadSafeNext(3600, 28800);

        return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
    }

    /// <summary>
    /// 获取稳定的缓存过期时间（60 ~ 120 天）
    /// </summary>
    /// <returns></returns>
    public static TimeSpan GetSteadyCacheExpireTime()
    {
        // （60 ~ 120 天）
        var totalSeconds = 12 * RandomEx.ThreadSafeNext(3600, 7200) * 120;

        return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
    }

    #endregion
}