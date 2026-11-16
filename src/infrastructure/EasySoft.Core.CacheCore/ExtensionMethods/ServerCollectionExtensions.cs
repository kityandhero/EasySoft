using EasySoft.Core.CacheCore.Assists;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.CacheCore.ExtensionMethods;

public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入缓存内核
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration">配置集</param>
    /// <returns>服务集</returns>
    public static IServiceCollection AddCacheCore(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new CacheConfigureAssist(configuration));

        services.AddCacheKeyAssist();

        return services;
    }

    private static IServiceCollection AddCacheKeyAssist(
        this IServiceCollection services
    )
    {
        services.AddSingleton<CacheKeyAssist>();

        return services;
    }
}