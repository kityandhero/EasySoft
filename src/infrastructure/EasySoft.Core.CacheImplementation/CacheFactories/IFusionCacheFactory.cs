namespace EasySoft.Core.CacheImplementation.CacheFactories;

public interface IFusionCacheFactory
{
    /// <summary>
    /// 根据缓存名称获取 FusionCache 实例
    /// </summary>
    /// <param name="cacheName">缓存名称，hybrid / memory</param>
    IFusionCache GetCache(string cacheName);
}