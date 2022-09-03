using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasyCaching.Core;
using EasyCaching.CSRedis;
using EasyCaching.InMemory;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.Enums;
using EasySoft.Core.EasyCaching.ExtensionMethods;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Core.EasyCaching.Operators;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.EasyCaching.Assists;

/// <summary>
/// 该方法仅用于控制台等场景
/// </summary>
public static class CacheOperatorAssist
{
    public static IServiceProvider CreateServiceProvider(
        AutofacServiceProviderFactory serviceProviderFactory,
        Action<ContainerBuilder> action
    )
    {
        IServiceCollection services = new ServiceCollection();

        var cacheMode = GeneralConfigAssist.GetCacheMode();

        if (cacheMode == CacheModeCollection.InMemory.ToString())
        {
            services.AddEasyCaching(options =>
            {
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
        }
        else if (cacheMode == CacheModeCollection.Redis.ToString())
        {
            services.AddEasyCaching(options =>
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
        }
        else
        {
            throw new Exception("not found available cache mode");
        }

        var containerBuilder = serviceProviderFactory.CreateBuilder(services);

        if (cacheMode == CacheModeCollection.InMemory.ToString())
        {
            containerBuilder.InterceptEasyCachingInMemory();

            containerBuilder.RegisterType<MemoryCacheOperator>().As<IAsyncCacheOperator>().SingleInstance();
        }
        else if (cacheMode == CacheModeCollection.Redis.ToString())
        {
            containerBuilder.InterceptEasyCachingCsRedis();

            containerBuilder.RegisterType<RedisCacheOperator>().As<IAsyncCacheOperator>().SingleInstance();

            containerBuilder.RegisterType<RedisFeatureCacheOperator>().As<IRedisFeatureCacheOperator>()
                .SingleInstance();
        }
        else
        {
            throw new Exception("not found available cache mode");
        }

        action(containerBuilder);

        return serviceProviderFactory.CreateServiceProvider(containerBuilder);
    }
}