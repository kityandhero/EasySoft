﻿using Autofac;
using EasySoft.Core.AccessWayTransmitter.Producers;
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
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<AccessWayProducer>().As<IAccessWayProducer>().SingleInstance();
        });

        return builder;
    }
}