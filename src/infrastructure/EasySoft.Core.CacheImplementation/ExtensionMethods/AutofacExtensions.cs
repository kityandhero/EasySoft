using EasySoft.Core.CacheImplementation.CacheFactories;
using EasySoft.Core.CacheImplementation.CacheKeyGenerators;
using EasySoft.Core.CacheImplementation.Interceptors;
using EasySoft.Core.CacheImplementation.Options;

namespace EasySoft.Core.CacheImplementation.ExtensionMethods;

public static class AutofacExtensions
{
    public static void InterceptCacheImplementation(
        this ContainerBuilder containerBuilder,
        Action<FusionCacheInterceptorOptions> action = null
    )
    {
        // 全局配置，一次性注册
        var fusionInterceptorOptions = new FusionCacheInterceptorOptions
        {
            CacheName = "fusion_interceptor_options",
            IsHighAvailability = true,
            DefaultExpirationSeconds = 300
        };

        containerBuilder.RegisterInstance(fusionInterceptorOptions);

        containerBuilder.RegisterType<DefaultFusionCacheFactory>()
            .As<IFusionCacheFactory>()
            .SingleInstance();

        containerBuilder.RegisterType<DefaultFusionCacheKeyGenerator>()
            .As<IFusionCacheKeyGenerator>()
            .SingleInstance();

        containerBuilder.RegisterType<FusionCacheInterceptor>()
            .As<IInterceptor>()
            .SingleInstance();

        var config = new FusionCacheInterceptorOptions();

        action?.Invoke(config);
    }
}