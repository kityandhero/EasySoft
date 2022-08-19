using Autofac;
using EasySoft.Core.GeneralLogTransmitter.Producers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.GeneralLogTransmitter.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseGeneralLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<GeneralLogProducer>().As<IGeneralLogProducer>().SingleInstance();
        });

        return builder;
    }
}