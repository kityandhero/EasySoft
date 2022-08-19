using Autofac;
using EasySoft.Core.ErrorLogTransmitter.Producers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseErrorLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<ErrorLogProducer>().As<IErrorLogProducer>().SingleInstance();
        });

        return builder;
    }
}