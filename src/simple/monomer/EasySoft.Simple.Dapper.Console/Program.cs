﻿// See https://aka.ms/new-console-template for more information

using Autofac;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Dapper.Elegant.Assists;
using EasySoft.Core.Dapper.Elegant.Configure;
using EasySoft.Core.EasyCaching.ExtensionMethods;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Core.EasyCaching.Operators;
using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;
using EasySoft.Simple.Dapper.Console;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Assists;
using CacheModeCollection = EasySoft.Core.EasyCaching.Enums.CacheModeCollection;

AutoFacConsoleAssist.CreateServiceProvider(
    services =>
    {
        var cacheMode = GeneralConfigAssist.GetCacheMode();

        if (cacheMode == CacheModeCollection.InMemory.ToString())
        {
            services.AddAdvanceEasyCachingInMemory();
        }
        else if (cacheMode == CacheModeCollection.Redis.ToString())
        {
            services.AddAdvanceEasyCachingRedis();
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

            containerBuilder.RegisterType<RedisFeatureCacheOperator>()
                .As<IRedisFeatureCacheOperator>()
                .SingleInstance();
        }
        else
        {
            throw new Exception("not found available cache mode");
        }

        containerBuilder.RegisterInstance(
                new ApplicationChannel().SetChannel(ChannelCollection.DapperTestApplication)
            )
            .As<IApplicationChannel>()
            .SingleInstance();
    }
);

DapperElegantConfigurator.SetCacheOperator(AutofacAssist.Instance.Resolve<IAsyncCacheOperator>());

var author = EntityAssist.GetEntity<AccessWay>(1767475361169412096);

if (author != null)
{
    Console.WriteLine(JsonConvertAssist.SerializeObject(author));
}