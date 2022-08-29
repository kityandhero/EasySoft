using System.Reflection;
using Autofac;
using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.AutoMapper.ExtensionMethods;

// https://github.com/alsami/AutoMapper.Contrib.Autofac.DependencyInjection

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 增加转换器  
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    /// <param name="assembly"></param>
    /// <param name="propertiesAutowired"></param>
    public static WebApplicationBuilder AddAdvanceAutoMapper(
        this WebApplicationBuilder builder,
        Assembly assembly,
        bool propertiesAutowired = false
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterAutoMapper(assembly, propertiesAutowired);
        });

        FlagAssist.SetAutoMapperSwitchOpen();

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceAutoMapper(
        this WebApplicationBuilder builder,
        bool propertiesAutowired = false
    )
    {
        return builder.AddAdvanceAutoMapper(propertiesAutowired, AppDomain.CurrentDomain.GetAssemblies());
    }

    public static WebApplicationBuilder AddAdvanceAutoMapper(
        this WebApplicationBuilder builder,
        bool propertiesAutowired,
        Assembly[] assemblies
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterAutoMapper(
                propertiesAutowired,
                assemblies
            );
        });

        FlagAssist.SetAutoMapperSwitchOpen();

        return builder;
    }
}