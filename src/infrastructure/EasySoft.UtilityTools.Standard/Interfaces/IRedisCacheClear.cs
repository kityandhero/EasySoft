namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// Redis 缓存清楚消息
/// </summary>
public interface IRedisCacheClear
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public string CacheKey { get; set; }

    /// <summary>
    /// 匹配模式
    /// </summary>
    public bool MatchMode { get; set; }
}