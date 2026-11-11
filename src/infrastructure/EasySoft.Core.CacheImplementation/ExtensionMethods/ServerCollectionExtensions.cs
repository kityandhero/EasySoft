using EasySoft.Core.CacheImplementation.Operators;

namespace EasySoft.Core.CacheImplementation.ExtensionMethods;

public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入内存以及Redis二级缓存
    /// </summary>
    /// <param name="services">services</param>
    /// <returns></returns>
    public static IServiceCollection AddAdvanceCacheImplementation(
        this IServiceCollection services
    )
    {
        services.AddMemoryCacheOperator();
        services.AddRedisCacheOperator();

        services.AddSingleton<GeneralCacheOperator>();

        services.AddSingleton<ICacheOperator>(sp => sp.GetRequiredService<GeneralCacheOperator>());

        return services;
    }

    internal static IServiceCollection AddMemoryCacheOperator(
        this IServiceCollection services
    )
    {
        services.AddSingleton<MemoryCacheOperator>();

        return services;
    }

    internal static IServiceCollection AddRedisCacheOperator(
        this IServiceCollection services
    )
    {
        services.AddSingleton<RedisCacheOperator>();

        return services;
    }
}