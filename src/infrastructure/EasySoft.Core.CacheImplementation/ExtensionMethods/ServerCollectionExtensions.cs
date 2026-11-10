using EasySoft.Core.CacheImplementation.CacheFactories;
using EasySoft.Core.CacheImplementation.CacheKeyGenerators;
using EasySoft.Core.CacheImplementation.ConfigAssist;
using EasySoft.Core.CacheImplementation.Interceptors;
using EasySoft.Core.CacheImplementation.Operators;
using EasySoft.Core.CacheImplementation.Options;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.Core.CacheImplementation.ExtensionMethods;

public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入内存以及Redis二级缓存
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAdvanceCacheImplementation(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.InterceptCacheImplementation();

        const string redis = nameof(CacheModeCollection.Redis);

        var cacheModeNode = RedisConfigAssist.GetCacheMode();

        var cacheMode = configuration[cacheModeNode]
                        ?? throw new InvalidOperationException(
                            $"缺少 Cache Mode 配置 inMemory/redis [{cacheModeNode}]。"
                        );

        var redisConnectionStringNode = RedisConfigAssist.GetRedisConnectionString();

        var redisConnection = "";

        if (cacheMode.In(redis.ToLower()))
        {
            redisConnection = configuration[redisConnectionStringNode]
                              ?? throw new InvalidOperationException(
                                  $"缺少 Cache Redis Connection 配置[{redisConnectionStringNode}]。"
                              );
        }

        if (!string.IsNullOrWhiteSpace(redisConnection))
        {
            StartupDescriptionMessageAssist.AddPrompt(
                $"Redis Connections: {redisConnection}."
            );
        }

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
                    // 根据参数，是否跳过内存缓存
                    o.SetSkipMemoryCache(cacheMode.In(redis.ToLower()));
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

        services.AddSingleton(serviceProvider =>
            {
                var fusionCacheFactory = serviceProvider.GetRequiredService<IFusionCacheFactory>();

                var globalPrefix = AppSettingAssist.GetGlobalPrefix(configuration);

                return new GeneralCacheOperator(fusionCacheFactory, globalPrefix);
            }
        );

        services.AddSingleton<ICacheOperator>(sp => sp.GetRequiredService<GeneralCacheOperator>());
        services.AddSingleton<IAsyncCacheOperator>(sp => sp.GetRequiredService<GeneralCacheOperator>());

        return services;
    }

    private static void InterceptCacheImplementation(
        this IServiceCollection services,
        Action<FusionCacheInterceptorOptions> action = null
    )
    {
        // 全局配置，一次性注册
        var fusionInterceptorOptions = new FusionCacheInterceptorOptions
        {
            CacheName = "fusion_interceptor_options",
            IsHighAvailability = true,
            DefaultExpirationSeconds = 300
        };

        // 对应 RegisterInstance：Autofac 中默认 AsSelf，即注册为 FusionCacheInterceptorOptions 类型
        services.AddSingleton(fusionInterceptorOptions);

        services.AddSingleton<IFusionCacheFactory, DefaultFusionCacheFactory>();
        services.AddSingleton<IFusionCacheKeyGenerator, DefaultFusionCacheKeyGenerator>();
        services.AddSingleton<IInterceptor, FusionCacheInterceptor>();

        var config = new FusionCacheInterceptorOptions();

        action?.Invoke(config);
    }
}