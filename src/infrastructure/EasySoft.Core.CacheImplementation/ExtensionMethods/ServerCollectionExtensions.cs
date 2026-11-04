namespace EasySoft.Core.CacheImplementation.ExtensionMethods;

public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入内存以及Redis二级缓存
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="whetherSkipMemoryCache">whether skip memory cache</param>
    /// <param name="redisConnection">redis connection string</param>
    /// <returns></returns>
    public static IServiceCollection AddAdvanceCacheImplementation(
        this IServiceCollection services,
        bool whetherSkipMemoryCache,
        string redisConnection
    )
    {
        // ========== 自定义 MemoryCache（给 hybrid 实例用，限制L1内存） ==========
        services.AddSingleton<IMemoryCache>(_ =>
            {
                // 容量上限，每个缓存Set时设置Size=1，就代表最多1000条
                var memoryCacheOptions = new MemoryCacheOptions
                {
                    SizeLimit = 1000
                };

                return new MemoryCache(memoryCacheOptions);
            }
        );

        var fusionCacheBuilder = services
            .AddFusionCache(ConstCollection.FusionCacheName)
            .WithNewtonsoftJsonSerializer()
            .WithDefaultEntryOptions(o =>
                {
                    // 内存过期
                    o.MemoryCacheDuration = TimeSpan.FromMinutes(2);

                    // Redis过期
                    o.DistributedCacheDuration = TimeSpan.FromMinutes(10);

                    // 根据参数，是否跳过内存缓存
                    o.SetSkipMemoryCache(whetherSkipMemoryCache);
                }
            );

        // builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        // builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        // {
        //     // 这一行至关重要，缺少就会出现当前报错
        //     containerBuilder.Populate(builder.Services);
        //
        //     // 你的EasySoft框架模块、GeneralCacheOperator注册
        //     containerBuilder.RegisterModule<EasySoftModule>();
        // });

        if (!string.IsNullOrWhiteSpace(redisConnection))
        {
            var muxer = ConnectionMultiplexer.Connect(redisConnection);

            // 注册连接复用器（后面裸Redis操作RemoveByPrefix用）
            services.AddSingleton(muxer);
            services.AddSingleton(muxer.GetDatabase());

            // 注册Redis IDistributedCache，给FusionCache当L2
            services.AddStackExchangeRedisCache(options =>
                {
                    options.ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(muxer);
                }
            );

            // 构造 RedisBackplane
            var redisBackplane = new RedisBackplane(
                new RedisBackplaneOptions
                {
                    ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(muxer)
                }
            );

            fusionCacheBuilder.WithDistributedCache(o => o.GetRequiredService<IDistributedCache>())
                .WithBackplane(redisBackplane);
        }

        return services;
    }
}