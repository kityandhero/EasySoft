namespace EasySoft.Core.CacheImplementation.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class FusionCacheAbleAttribute : Attribute
{
    /// <summary>
    /// 缓存Key模板，支持参数
    /// </summary>
    public string CacheKey { get; set; }

    /// <summary>
    /// 过期时间(秒)，优先级高于全局Options
    /// </summary>
    public int ExpirationSeconds { get; set; }

    /// <summary>
    /// 指定缓存实例名称，覆盖全局
    /// </summary>
    public string CacheName { get; set; } = "default_cache_instance";
}