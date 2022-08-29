using System.Reflection;
using AspectCore.Configuration;
using AspectCore.Extensions.Autofac;
using Autofac;
using EasyCaching.Core;
using EasyCaching.Core.Configurations;
using EasyCaching.Core.Interceptor;
using EasyCaching.CSRedis;
using EasyCaching.InMemory;
using EasyCaching.Interceptor.AspectCore;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.Enums;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Core.EasyCaching.Operators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
        {
            builder.AddAdvanceEasyCachingInMemory();
        }
        else if (cacheMode == CacheModeCollection.Redis.ToString())
        {
            builder.AddAdvanceEasyCachingCsRedis();
        }
        else
        {
            throw new Exception("not found available cache mode");
        }

        return builder;
    }

    /// <summary>
    /// Add the AspectCore interceptor.
    /// </summary>
    public static WebApplicationBuilder AddEasyCachingInterceptor(
        this WebApplicationBuilder builder,
        Action<EasyCachingInterceptorOptions> action
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<DefaultEasyCachingKeyGenerator>().As<IEasyCachingKeyGenerator>();

            containerBuilder.RegisterType<EasyCachingInterceptor>();

            var config = new EasyCachingInterceptorOptions();

            action(config);

            var options = Options.Create(config);

            containerBuilder.Register(x => options);

            containerBuilder.RegisterDynamicProxy(configure =>
            {
                bool All(MethodInfo x) => x.CustomAttributes.Any(data =>
                    typeof(EasyCachingInterceptorAttribute).GetTypeInfo().IsAssignableFrom(data.AttributeType)
                );

                configure.Interceptors.AddTyped<EasyCachingInterceptor>(All);
            });
        });

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceEasyCachingInMemory(
        this WebApplicationBuilder builder
    )
    {
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
        //Important step for In-Memory Caching
        builder.Services.AddEasyCaching(options =>
        {
            // use memory cache with your own configuration
            options.UseInMemory(config =>
                {
                    config.DBConfig = new InMemoryCachingOptions
                    {
                        // scan time, default value is 60s
                        ExpirationScanFrequency = 60,
                        // total count of cache items, default value is 10000
                        SizeLimit = 100,

                        // below two settings are added in v0.8.0
                        // enable deep clone when reading object from cache or not, default value is true.
                        EnableReadDeepClone = true,
                        // enable deep clone when writing object to cache or not, default valuee is false.
                        EnableWriteDeepClone = false,
                    };
                    // the max random second will be added to cache's expiration, default value is 120
                    config.MaxRdSecond = 120;
                    // whether enable logging, default is false
                    config.EnableLogging = false;
                    // mutex key's alive time(ms), default is 5000
                    config.LockMs = 5000;
                    // when mutex key alive, it will sleep some time, default is 300
                    config.SleepMs = 300;
                },
                ConstCollection.DefaultInMemoryCachingName
            );
        });

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
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<MemoryCacheOperator>().As<IAsyncCacheOperator>().SingleInstance();
        });

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceEasyCachingCsRedis(
        this WebApplicationBuilder builder
    )
    {
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
        //Important step for In-Memory Caching
        builder.Services.AddEasyCaching(options =>
        {
            options.UseCSRedis(
                    config =>
                    {
                        config.DBConfig = new CSRedisDBOptions
                        {
                            ConnectionStrings = RedisConfigAssist.GetConnectionCollection(),
                            // the sentinels settings
                            Sentinels = RedisConfigAssist.GetSentinelCollection(),
                            // the read write setting for sentinel mode
                            ReadOnly = false
                        };

                        config.SerializerName = ConstCollection.SerializerName;
                    },
                    ConstCollection.DefaultRedisCachingName
                )
                .WithJson(ConstCollection.SerializerName);
        });

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
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<RedisCacheOperator>().As<IAsyncCacheOperator>().SingleInstance();

            containerBuilder.RegisterType<RedisFeatureCacheOperator>().As<IRedisFeatureCacheOperator>()
                .SingleInstance();
        });

        return builder;
    }
}