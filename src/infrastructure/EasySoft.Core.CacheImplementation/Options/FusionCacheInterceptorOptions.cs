namespace EasySoft.Core.CacheImplementation.Options;

public class FusionCacheInterceptorOptions
{
    /// <summary>
    /// 默认缓存实例名称（Fusion支持多命名实例）
    /// </summary>
    public string CacheName { get; set; } = "default_cache_instance";

    /// <summary>
    /// 高可用：缓存异常时，直接执行原方法，不抛出异常（对标IsHighAvailability）
    /// </summary>
    public bool IsHighAvailability { get; set; } = true;

    /// <summary>
    /// 默认过期时间（秒），方法Attribute不指定时使用全局默认值
    /// </summary>
    public int DefaultExpirationSeconds { get; set; } = 60;
}