using EasySoft.Core.CacheCore.Assists;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.CacheCore.ExtensionMethods;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddCacheCore(
        this IServiceCollection services
    )
    {
        services.AddCacheConfigureAssist();
        services.AddCacheKeyAssist();

        return services;
    }

    private static IServiceCollection AddCacheConfigureAssist(
        this IServiceCollection services
    )
    {
        services.AddSingleton<CacheConfigureAssist>();

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