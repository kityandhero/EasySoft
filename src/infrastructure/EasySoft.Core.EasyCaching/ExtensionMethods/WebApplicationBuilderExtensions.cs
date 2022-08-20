using Autofac;
using EasyCaching.CSRedis;
using EasyCaching.InMemory;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.Enums;
using EasySoft.Core.EasyCaching.Operators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.EasyCaching.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置缓存模式
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder UseAdvanceEasyCaching(
        this WebApplicationBuilder builder
    )
    {
        var cacheMode = GeneralConfigAssist.GetCacheMode();

        if (cacheMode == CacheModeCollection.InMemory.ToString())
        {
            builder.UseAdvanceEasyCachingInMemory();
        }
        else if (cacheMode == CacheModeCollection.Redis.ToString())
        {
            builder.UseAdvanceEasyCachingCsRedis();
        }
        else
        {
            throw new Exception("not found available cache mode");
        }

        return builder;
    }

    private static WebApplicationBuilder UseAdvanceEasyCachingInMemory(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddEasyCaching(options =>
        {
            options.UseInMemory("default");

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
            }, "defaultOne");
        });

        builder.UseMemoryCacheOperatorInjection();

        return builder;
    }

    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder UseMemoryCacheOperatorInjection(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<MemoryCacheOperator>().As<ICacheOperator>().SingleInstance();
        });

        return builder;
    }

    private static WebApplicationBuilder UseAdvanceEasyCachingCsRedis(
        this WebApplicationBuilder builder
    )
    {
        //Important step for Redis Caching
        builder.Services.AddEasyCaching(option =>
        {
            option.UseCSRedis(config =>
            {
                var o = config.DBConfig = new CSRedisDBOptions
                {
                    ConnectionStrings = RedisConfigAssist.GetConnectionCollection(),
                    // the read write setting for sentinel mode
                    ReadOnly = false
                };

                if (RedisConfigAssist.GetSentinelCollection().Any())
                {
                    // the sentinels settings
                    o.Sentinels = RedisConfigAssist.GetSentinelCollection();
                }

                config.DBConfig = o;
            });
        });

        builder.UseRedisCacheOperatorInjection();

        return builder;
    }

    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder UseRedisCacheOperatorInjection(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<RedisCacheOperator>().As<ICacheOperator>().SingleInstance();
        });

        return builder;
    }
}