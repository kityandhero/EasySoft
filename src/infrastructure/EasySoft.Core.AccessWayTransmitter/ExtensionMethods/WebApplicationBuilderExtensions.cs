using Autofac;
using DotNetCore.CAP.Filter;
using EasySoft.Core.AccessWayTransmitter.Assists;
using EasySoft.Core.AccessWayTransmitter.Producers;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.AccessWayTransmitter.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAccessWayTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (InitialAssist.InitialComplete)
        {
            return builder;
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<AccessWayProducer>().As<IAccessWayProducer>().SingleInstance();
        });

        InitialAssist.InitialComplete = true;

        return builder;
    }
}