using EasySoft.Core.CacheImplementation.Operators;
using EasySoft.Core.CacheImplementation.Options;

namespace EasySoft.Core.CacheImplementation.ExtensionMethods;

public static class ConfigureHostBuilderExtensions
{
    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal static ConfigureHostBuilder AddAdvanceCacheImplementationOperatorInjection(
        this ConfigureHostBuilder builder
    )
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<GeneralCacheOperator>()
                    .As<IAsyncCacheOperator>()
                    .SingleInstance()
                    .AsSelf();
            }
        );

        return builder;
    }

    /// <summary>
    /// Add the AspectCore interceptor.
    /// </summary>
    internal static ConfigureHostBuilder AddAdvanceCacheImplementationInterceptor(
        this ConfigureHostBuilder builder,
        Action<FusionCacheInterceptorOptions> action
    )
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.InterceptCacheImplementation(action);
            }
        );

        return builder;
    }
}