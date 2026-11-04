namespace EasySoft.Core.CacheImplementation.CacheFactories;

public class DefaultFusionCacheFactory : IFusionCacheFactory
{
    private readonly IFusionCacheProvider _fusionCacheProvider;

    public DefaultFusionCacheFactory(IFusionCacheProvider fusionCacheProvider)
    {
        _fusionCacheProvider = fusionCacheProvider;
    }    

    public IFusionCache GetCache(string cacheName)
    {
        var cache = _fusionCacheProvider.GetCacheOrNull(cacheName);
        
        if (cache is null)
        {
            throw new InvalidOperationException($"FusionCache [{cacheName}] 没有注册，请检查services.AddFusionCache");
        }
        
        return cache;
    }
}   