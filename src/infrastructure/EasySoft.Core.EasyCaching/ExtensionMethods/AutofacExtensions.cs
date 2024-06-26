﻿namespace EasySoft.Core.EasyCaching.ExtensionMethods;

public static class AutofacExtensions
{
    internal static void InterceptEasyCaching(
        this ContainerBuilder containerBuilder,
        Action<EasyCachingInterceptorOptions> action
    )
    {
        containerBuilder.RegisterType<DefaultEasyCachingKeyGenerator>().As<IEasyCachingKeyGenerator>();

        containerBuilder.RegisterType<EasyCachingInterceptor>();

        var config = new EasyCachingInterceptorOptions();

        action(config);

        var options = Options.Create(config);

        containerBuilder.Register(x => options);

        containerBuilder.RegisterDynamicProxy(configure =>
        {
            bool All(MethodInfo x)
            {
                return x.CustomAttributes.Any(data =>
                    typeof(EasyCachingInterceptorAttribute).GetTypeInfo().IsAssignableFrom(data.AttributeType)
                );
            }

            configure.Interceptors.AddTyped<EasyCachingInterceptor>(All);
        });
    }

    public static void InterceptEasyCachingInMemory(
        this ContainerBuilder containerBuilder
    )
    {
        containerBuilder.InterceptEasyCaching(x =>
            x.CacheProviderName = EasyCachingConstValue.DefaultInMemoryName
        );
    }

    public static void InterceptEasyCachingCsRedis(
        this ContainerBuilder containerBuilder
    )
    {
        containerBuilder.InterceptEasyCaching(x =>
            x.CacheProviderName = EasyCachingConstValue.DefaultRedisName
        );
    }
}