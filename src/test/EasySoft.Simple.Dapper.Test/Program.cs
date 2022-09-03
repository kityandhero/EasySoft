// See https://aka.ms/new-console-template for more information

using Autofac;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Dapper.Elegant.Assists;
using EasySoft.Simple.Dapper.Test.Entities;
using EasySoft.Simple.Dapper.Test.Enums;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Assists;

Console.WriteLine("Hello, World!");

var containerBuilder = new ContainerBuilder();

containerBuilder.RegisterInstance(
        new ApplicationChannel()
            .SetChannel(ApplicationChannelCollection.DapperTestApplication.ToInt())
            .SetName(ApplicationChannelCollection.DapperTestApplication.GetDescription())
    )
    .As<IApplicationChannel>().SingleInstance();

var container = containerBuilder.Build();

AutofacAssist.Instance.SetContainer(container);

var author = EntityAssist.GetEntity<Author>(1);

if (author != null)
{
    Console.WriteLine(JsonConvertAssist.Serialize(author));
}