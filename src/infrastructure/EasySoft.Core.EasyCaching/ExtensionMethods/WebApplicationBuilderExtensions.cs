using EasyCaching.Core;
using EasyCaching.Core.Configurations;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.Enums;
using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.EasyCaching.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置缓存模式
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceEasyCaching(
        this WebApplicationBuilder builder
    )
    {
        var cacheMode = GeneralConfigAssist.GetCacheMode();

        if (cacheMode == CacheModeCollection.InMemory.ToString())
            builder.AddAdvanceEasyCachingInMemory();
        else if (cacheMode == CacheModeCollection.Redis.ToString())
            builder.AddAdvanceEasyCachingCsRedis();
        else
            throw new Exception("not found available cache mode");

        return builder;
    }

    /// <summary>
    /// Add the AspectCore interceptor.
    /// </summary>
    private static WebApplicationBuilder AddEasyCachingInterceptor(
        this WebApplicationBuilder builder,
        Action<EasyCachingInterceptorOptions> action
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddEasyCachingInterceptor)}()."
        );

        builder.Host.AddEasyCachingInterceptor(action);

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceEasyCachingInMemory(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAdvanceEasyCachingInMemory)}()."
        );

        builder.AddEasyCachingInMemoryCaching()
            .AddEasyCachingInterceptor(x =>
                x.CacheProviderName = EasyCachingConstValue.DefaultInMemoryName
            )
            .AddMemoryCacheOperatorInjection();

        return builder;
    }

    /// <summary>
    /// 注入内存缓存
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddEasyCachingInMemoryCaching(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddEasyCachingInMemoryCaching)}()."
        );

        //Important step for In-Memory Caching
        builder.Services.AddEasyCachingInMemoryCaching();

        return builder;
    }

    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddMemoryCacheOperatorInjection(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddMemoryCacheOperatorInjection)}()."
        );

        builder.Host.AddMemoryCacheOperatorInjection();

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceEasyCachingCsRedis(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAdvanceEasyCachingCsRedis)}()."
        );

        //Important step for Redis Caching
        builder.AddEasyCachingRedisCaching()
            .AddEasyCachingInterceptor(x =>
                x.CacheProviderName = EasyCachingConstValue.DefaultRedisName
            )
            .AddRedisCacheOperatorInjection();

        return builder;
    }

    /// <summary>
    /// 注入Redis缓存
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddEasyCachingRedisCaching(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddEasyCachingRedisCaching)}()."
        );

        //Important step for In-Memory Caching
        builder.Services.AddEasyCachingRedisCaching();

        return builder;
    }

    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddRedisCacheOperatorInjection(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddRedisCacheOperatorInjection)}()."
        );

        builder.Host.AddRedisCacheOperatorInjection();

        return builder;
    }
}