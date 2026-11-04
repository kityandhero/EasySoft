// See https://aka.ms/new-console-template for more information

using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.CacheImplementation.ConfigAssist;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.CacheImplementation.Operators;
using EasySoft.Core.Dapper.Elegant.Assists;
using EasySoft.Core.Dapper.Elegant.Configure;
using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;
using EasySoft.Simple.Dapper.Console;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Extensions;

AutoFacConsoleAssist.CreateServiceProvider(
    services =>
    {
        var redisConnection = RedisConfigAssist.GetConnectionCollection().Join("|");

        services.AddAdvanceCacheImplementation(false, redisConnection);
    },
    containerBuilder =>
    {
        containerBuilder.InterceptCacheImplementation();

        containerBuilder.RegisterType<GeneralCacheOperator>().As<IAsyncCacheOperator>().SingleInstance();

        containerBuilder.RegisterInstance(
                new ApplicationChannel().SetChannel(ChannelCollection.DapperTestApplication)
            )
            .As<IApplicationChannel>()
            .SingleInstance();
    }
);

DapperElegantConfigurator.SetCacheOperator(AutofacAssist.Instance.Resolve<IAsyncCacheOperator>());

var author = EntityAssist.GetEntity<AccessWay>(1);

if (author != null)
{
    Console.WriteLine(JsonConvertAssist.SerializeObject(author));
}