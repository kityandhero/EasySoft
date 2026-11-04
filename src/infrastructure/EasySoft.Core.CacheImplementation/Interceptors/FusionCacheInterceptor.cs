using EasySoft.Core.CacheImplementation.Attributes;
using EasySoft.Core.CacheImplementation.CacheFactories;
using EasySoft.Core.CacheImplementation.CacheKeyGenerators;
using EasySoft.Core.CacheImplementation.Options;

namespace EasySoft.Core.CacheImplementation.Interceptors;

public class FusionCacheInterceptor : IInterceptor
{
    private readonly FusionCacheInterceptorOptions _options;
    private readonly IFusionCacheFactory _fusionCacheFactory;
    private readonly IFusionCacheKeyGenerator _keyGenerator;

    public FusionCacheInterceptor(
        FusionCacheInterceptorOptions options,
        IFusionCacheFactory fusionCacheFactory,
        IFusionCacheKeyGenerator keyGenerator
    )
    {
        _options = options;
        _fusionCacheFactory = fusionCacheFactory;
        _keyGenerator = keyGenerator;
    }

    public void Intercept(IInvocation invocation)
    {
        var attr = invocation.Method.GetCustomAttributes(true)
            .OfType<FusionCacheAbleAttribute>()
            .FirstOrDefault();

        if (attr == null)
        {
            invocation.Proceed();
            return;
        }

        var cacheName = string.IsNullOrWhiteSpace(attr.CacheName) ? _options.CacheName : attr.CacheName;
        var expireSec = attr.ExpirationSeconds > 0 ? attr.ExpirationSeconds : _options.DefaultExpirationSeconds;
        var fusionCache = _fusionCacheFactory.GetCache(cacheName);

        try
        {
            // 生成缓存key
            var cacheKey = BuildCacheKey(invocation, attr.CacheKey);

            var cachedVal = fusionCache.GetOrDefault<object>(cacheKey);
            if (cachedVal != null)
            {
                invocation.ReturnValue = cachedVal;
                return;
            }

            invocation.Proceed();
            var returnValue = invocation.ReturnValue;

            var opt = new FusionCacheEntryOptions { Duration = TimeSpan.FromSeconds(expireSec) };
            fusionCache.Set(
                cacheKey,
                returnValue,
                opt
            );
        }
        catch
        {
            if (_options.IsHighAvailability)
            {
                invocation.Proceed();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// 构建缓存Key
    /// </summary>
    private string BuildCacheKey(IInvocation invocation, string keyTemplate)
    {
        return keyTemplate;
    }
}