using EasySoft.Core.EasyCaching.ConfigAssist;

namespace EasySoft.Core.EasyCaching.ExtensionMethods;

public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入内存缓存
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAdvanceEasyCachingInMemory(
        this IServiceCollection services
    )
    {
        //Important step for In-Memory Caching
        services.AddEasyCaching(options =>
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
                        EnableWriteDeepClone = false
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

        return services;
    }

    /// <summary>
    /// 注入Redis缓存
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IServiceCollection AddAdvanceEasyCachingRedis(
        this IServiceCollection builder
    )
    {
        //Important step for In-Memory Caching
        builder.AddEasyCaching(options =>
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
}