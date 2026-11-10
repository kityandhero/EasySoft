// See https://aka.ms/new-console-template for more information

using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.CacheImplementation.Operators;
using EasySoft.Core.Dapper.Elegant.Assists;
using EasySoft.Core.Dapper.Elegant.Configure;
using EasySoft.Core.DependencyInjection.IocAssists;
using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;
using EasySoft.Simple.Dapper.Console;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = ConsoleServiceProviderAssist.CreateServiceProvider((serviceCollection, configuration) =>
    {
        serviceCollection.AddAdvanceCacheImplementation(configuration);

        // *) RegisterInstance(new ApplicationChannel().SetChannel(...))
        //       .As<IApplicationChannel>().SingleInstance()
        //    → 实例单例注册
        serviceCollection.AddSingleton<IApplicationChannel>(
            new ApplicationChannel().SetChannel(ChannelCollection.DapperTestApplication)
        );
    }
);

DapperElegantConfigurator.SetCacheOperator(serviceProvider.GetRequiredService<IAsyncCacheOperator>());

var author = EntityAssist.GetEntity<AccessWay>(1);

if (author != null)
{
    Console.WriteLine(JsonConvertAssist.SerializeObject(author));
}