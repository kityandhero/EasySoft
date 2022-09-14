﻿using Autofac;
using EasyCaching.Core.Configurations;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Core.EasyCaching.Operators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.EasyCaching.ExtensionMethods;

public static class ConfigureHostBuilderExtensions
{
    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal static ConfigureHostBuilder AddMemoryCacheOperatorInjection(
        this ConfigureHostBuilder builder
    )
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<MemoryCacheOperator>().As<IAsyncCacheOperator>().SingleInstance();
        });

        return builder;
    }

    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal static ConfigureHostBuilder AddRedisCacheOperatorInjection(
        this ConfigureHostBuilder builder
    )
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<RedisCacheOperator>().As<IAsyncCacheOperator>().SingleInstance();

            containerBuilder.RegisterType<RedisFeatureCacheOperator>().As<IRedisFeatureCacheOperator>()
                .SingleInstance();
        });

        return builder;
    }

    /// <summary>
    /// Add the AspectCore interceptor.
    /// </summary>
    internal static ConfigureHostBuilder AddEasyCachingInterceptor(
        this ConfigureHostBuilder builder,
        Action<EasyCachingInterceptorOptions> action
    )
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.InterceptEasyCaching(action);
        });

        return builder;
    }
}