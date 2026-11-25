// See https://aka.ms/new-console-template for more information

using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Host.Framework.Kernel.Assists;
using EasySoft.Core.Dapper.Elegant.Assists;
using EasySoft.Core.Dapper.Elegant.Configure;
using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;
using EasySoft.Simple.Dapper.Console;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Channels;

var serviceProvider = HostBuilderAssist11.CreateServiceProvider(
    new ApplicationChannel().SetChannel(ChannelCollection.DapperTestApplication)
);

DapperElegantConfigurator.SetCacheOperator(serviceProvider.GetRequiredService<ICacheOperator>());

var author = EntityAssist.GetEntity<AccessWay>(1);

if (author != null)
{
    LoggerAssist.TraceData(author);

    // Console.WriteLine(JsonConvertAssist.SerializeObject(author));
}