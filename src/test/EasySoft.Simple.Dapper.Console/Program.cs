// See https://aka.ms/new-console-template for more information

using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Dapper.Elegant.Assists;
using EasySoft.Core.Dapper.Elegant.Configure;
using EasySoft.Core.EasyCaching.Assists;
using EasySoft.Simple.Dapper.Console.Entities;
using EasySoft.Simple.Dapper.Console.Enums;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Assists;

Console.WriteLine("Hello, World!");

var serviceProviderFactory = new AutofacServiceProviderFactory();

var serviceProvider = CacheOperatorAssist.CreateServiceProvider(
    serviceProviderFactory,
    containerBuilder =>
    {
        containerBuilder.RegisterInstance(
                new ApplicationChannel()
                    .SetChannel(ApplicationChannelCollection.DapperTestApplication.ToInt())
                    .SetName(ApplicationChannelCollection.DapperTestApplication.GetDescription())
            )
            .As<IApplicationChannel>().SingleInstance();
    }
);

AutofacAssist.Instance.SetContainer(serviceProvider.GetAutofacRoot());

DapperElegantConfigurator.SetCacheOperator(AutofacAssist.Instance.Resolve<IAsyncCacheOperator>());

var author = EntityAssist.GetEntity<Author>(1);

if (author != null)
{
    Console.WriteLine(JsonConvertAssist.Serialize(author));
}