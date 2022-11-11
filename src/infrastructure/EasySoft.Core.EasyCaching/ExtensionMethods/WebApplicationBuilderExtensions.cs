using EasyCaching.Core;
using EasyCaching.Core.Configurations;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.ConfigAssist;
using EasySoft.Core.EasyCaching.Enums;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.EasyCaching.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceEasyCaching = "b73d0680-f482-4168-b752-6f33abb10c0f";

    /// <summary>
    /// 配置缓存模式
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceEasyCaching(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceEasyCaching))
            return builder;

        StartupDescriptionMessageAssist.AddTraceDivider();

        RedisConfigAssist.Init();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceEasyCaching)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"CacheMode is {GeneralConfigAssist.GetCacheMode()}.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        if (GeneralConfigAssist.GetCacheMode() == CacheModeCollection.Redis.ToString())
            StartupDescriptionMessageAssist.AddPrompt(
                $"{CacheModeCollection.Redis.ToString()} Connections: {RedisConfigAssist.GetConnectionCollection().Join("|")}.",
                RedisConfigAssist.GetConfigFileInfo()
            );

        var cacheMode = GeneralConfigAssist.GetCacheMode();

        if (cacheMode == CacheModeCollection.InMemory.ToString())
            builder.AddAdvanceEasyCachingInMemoryMode();
        else if (cacheMode == CacheModeCollection.Redis.ToString())
            builder.AddAdvanceEasyCachingCsRedis();
        else
            throw new Exception("not found available cache mode");

        return builder;
    }

    /// <summary>
    /// Add the AspectCore interceptor.
    /// </summary>
    private static WebApplicationBuilder AddAdvanceEasyCachingInterceptor(
        this WebApplicationBuilder builder,
        Action<EasyCachingInterceptorOptions> action
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceEasyCachingInterceptor)}."
        );

        builder.Host.AddAdvanceEasyCachingInterceptor(action);

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceEasyCachingInMemoryMode(
        this WebApplicationBuilder builder
    )
    {
        builder.AddAdvanceEasyCachingInMemory()
            .AddAdvanceEasyCachingInterceptor(x =>
                x.CacheProviderName = EasyCachingConstValue.DefaultInMemoryName
            )
            .AddAdvanceEasyCachingInMemoryOperatorInjection();

        return builder;
    }

    /// <summary>
    /// 注入内存缓存
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddAdvanceEasyCachingInMemory(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceEasyCachingInMemory)}."
        );

        //Important step for In-Memory Caching
        builder.Services.AddAdvanceEasyCachingInMemory();

        return builder;
    }

    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddAdvanceEasyCachingInMemoryOperatorInjection(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceEasyCachingInMemoryOperatorInjection)}."
        );

        builder.Host.AddAdvanceEasyCachingInMemoryOperatorInjection();

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceEasyCachingCsRedis(
        this WebApplicationBuilder builder
    )
    {
        builder.AddAdvanceEasyCachingRedis()
            .AddAdvanceEasyCachingInterceptor(x =>
                x.CacheProviderName = EasyCachingConstValue.DefaultRedisName
            )
            .AddAdvanceEasyCachingRedisOperatorInjection();

        return builder;
    }

    /// <summary>
    /// 注入Redis缓存
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddAdvanceEasyCachingRedis(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceEasyCachingRedis)}."
        );

        //Important step for In-Memory Caching
        builder.Services.AddAdvanceEasyCachingRedis();

        return builder;
    }

    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddAdvanceEasyCachingRedisOperatorInjection(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceEasyCachingRedisOperatorInjection)}."
        );

        builder.Host.AddAdvanceEasyCachingRedisOperatorInjection();

        return builder;
    }
}