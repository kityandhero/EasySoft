using EasySoft.Core.CacheImplementation.ConfigAssist;
using EasySoft.Core.CacheImplementation.Options;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.Core.CacheImplementation.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceCacheImplementation = "b73d0680-f482-4168-b752-6f33abb10c0f";

    /// <summary>
    /// 配置缓存模式
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceCacheImplementation(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceCacheImplementation))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceCacheImplementation)}."
        );

        const string inMemory = nameof(CacheModeCollection.InMemory);
        const string redis = nameof(CacheModeCollection.Redis);
        const string hybrid = nameof(CacheModeCollection.Hybrid);

        var cacheMode = GeneralConfigAssist.GetCacheMode();

        var redisConnection = RedisConfigAssist.GetConnectionCollection().Join("|");

        if (cacheMode.In(
                redis,
                hybrid
            ) && string.IsNullOrWhiteSpace(redisConnection))
        {
            throw new ConfigErrorException(
                "请配置 Redis Connections",
                RedisConfigAssist.GetConfigFileInfo()
            );
        }

        if (!string.IsNullOrWhiteSpace(redisConnection))
        {
            RedisConfigAssist.Init();

            StartupDescriptionMessageAssist.AddPrompt(
                $"Redis Connections: {redisConnection}.",
                RedisConfigAssist.GetConfigFileInfo()
            );

            if (EnvironmentAssist.IsDevelopment() && AuxiliaryConfigure.PromptConfigFileInfo)
            {
                ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
                    new ExtraAction<IEndpointRouteBuilder>()
                        .SetName("")
                        .SetAction(endpoints => { endpoints.MapRedisConfigFile(); })
                );
            }
        }

        builder.Services.AddAdvanceCacheImplementation(
            cacheMode.In(
                inMemory,
                hybrid
            ),
            redisConnection
        );

        return builder;
    }

    /// <summary>
    /// Add the AspectCore interceptor.
    /// </summary>
    private static WebApplicationBuilder AddAdvanceCacheImplementationInterceptor(
        this WebApplicationBuilder builder,
        Action<FusionCacheInterceptorOptions> action
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceCacheImplementationInterceptor)}."
        );

        builder.Host.AddAdvanceCacheImplementationInterceptor(action);

        return builder;
    }

    /// <summary>
    /// 注入缓存操作者
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddAdvanceCacheImplementationOperatorInjection(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceCacheImplementationOperatorInjection)}."
        );

        builder.Host.AddAdvanceCacheImplementationOperatorInjection();

        return builder;
    }
}