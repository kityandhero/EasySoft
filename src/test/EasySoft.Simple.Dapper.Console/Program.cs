// See https://aka.ms/new-console-template for more information

using Autofac;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Dapper.Elegant.Assists;
using EasySoft.Core.Dapper.Elegant.Configure;
using EasySoft.Core.EasyCaching.Enums;
using EasySoft.Core.EasyCaching.ExtensionMethods;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Core.EasyCaching.Operators;
using EasySoft.Simple.Dapper.Console.Entities;
using EasySoft.Simple.Dapper.Console.Enums;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Assists;

var serviceProvider = AutoFacConsoleAssist.CreateServiceProvider(services =>
    {
        var cacheMode = GeneralConfigAssist.GetCacheMode();

        if (cacheMode == CacheModeCollection.InMemory.ToString())
        {
            services.AddEasyCachingInMemoryCaching();
        }
        else if (cacheMode == CacheModeCollection.Redis.ToString())
        {
            services.AddEasyCachingRedisCaching();
        }
        else
        {
            throw new Exception("not found available cache mode");
        }
    },
    containerBuilder =>
    {
        var cacheMode = GeneralConfigAssist.GetCacheMode();

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

        containerBuilder.RegisterInstance(
                new ApplicationChannel()
                    .SetChannel(ApplicationChannelCollection.DapperTestApplication.ToInt())
                    .SetName(ApplicationChannelCollection.DapperTestApplication.GetDescription())
            )
            .As<IApplicationChannel>().SingleInstance();
    }
);

DapperElegantConfigurator.SetCacheOperator(AutofacAssist.Instance.Resolve<IAsyncCacheOperator>());

var author = EntityAssist.GetEntity<Author>(1);

if (author != null)
{
    Console.WriteLine(JsonConvertAssist.Serialize(author));
}