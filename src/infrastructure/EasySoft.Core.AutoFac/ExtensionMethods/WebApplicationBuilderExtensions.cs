using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.AutoFac.Selectors;
using EasySoft.Core.Infrastructure.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.AutoFac.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceAutoFac(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(AutofacAssist.Init);

        builder.AddControllerPropertiesAutowired(Assembly.GetEntryAssembly());

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message =
                "You can set your autoFac config with autoFac.json in ./configures/autoFac.json. The document link is https://autofac.readthedocs.io/en/latest/configuration/xml.html."
        });

        return builder;
    }

    public static WebApplicationBuilder AddExtraNormalInjection(
        this WebApplicationBuilder builder,
        Action<ContainerBuilder> action
    )
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        builder.Host.ConfigureContainer(action);

        return builder;
    }

    public static WebApplicationBuilder AddControllerPropertiesAutowired(
        this WebApplicationBuilder builder,
        Assembly? assembly
    )
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            var controllerTypes = assembly.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .ToArray();

            // 配置所有控制器均支持属性注入
            containerBuilder.RegisterTypes(controllerTypes).PropertiesAutowired(new AutowiredPropertySelector());
        });

        return builder;
    }
}