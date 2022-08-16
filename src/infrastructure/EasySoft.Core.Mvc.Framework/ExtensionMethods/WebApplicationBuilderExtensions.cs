﻿using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Mvc.Framework.CommonAssists;
using EasySoft.Core.Mvc.Framework.IocAssists;
using EasySoft.Core.Mvc.Framework.PrepareWorks;
using EasySoft.Core.Mvc.Framework.Selectors;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Mvc.Framework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder OpenAutoFac(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(AutofacAssist.Init);

        builder.AddCovertInjection();

        builder.AddControllerPropertiesAutowired(Assembly.GetEntryAssembly());

        return builder;
    }

    /// <summary>
    /// 注入框架的启动预处理任务，启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCovertInjection(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<PrepareCovertStartWork>().As<IPrepareCovertStartWork>().SingleInstance();
        });

        return builder;
    }

    /// <summary>
    /// 注入定制的启动预处理任务，诸如任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddPrepareStartWorkInjection<T>(
        this WebApplicationBuilder builder
    ) where T : IPrepareStartWork
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<IPrepareStartWork>().SingleInstance();
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

    public static WebApplication EasyBuild(this WebApplicationBuilder builder)
    {
        return builder.EasyBuild(new List<string>());
    }

    public static WebApplication EasyBuild(this WebApplicationBuilder builder, List<string> areas)
    {
        var app = builder.Build();

        AutofacAssist.Instance.Container = app.UseHostFiltering().ApplicationServices.GetAutofacRoot();

        ServiceAssist.ServiceProvider = app.Services;

        // 中间件调用顺序请参阅: https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order

        app.UsePrepareStartWork();

        if (!app.Environment.IsDevelopment())
        {
            // app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAdvanceStaticFiles();

        app.UseRouting();

        if (GeneralConfigAssist.GetCorsEnable())
        {
            app.UseCors(ConstCollection.DefaultSpecificOrigins);

            app.RecordInformation(
                $"cors: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            app.RecordInformation(
                "cors: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        if (GeneralConfigAssist.GetUseAuthentication())
        {
            app.UseAuthentication();

            app.RecordInformation(
                $"UseAuthentication: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            app.RecordInformation(
                "UseAuthentication: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        if (GeneralConfigAssist.GetUseAuthorization())
        {
            app.UseAuthorization();

            app.RecordInformation(
                $"UseAuthorization: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            app.RecordInformation(
                "UseAuthorization: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        app.UseAdvanceSwagger();

        app.UseAdvanceHangfire();

        app.RecordInformation(
            "you can set your autoFac config with autoFac.json in ./configures/autoFac.json. The document link is https://autofac.readthedocs.io/en/latest/configuration/xml.html."
        );

        app.RecordInformation(
            "you can get all controller actions by visit https://[host]:[port]/[controller]/getAllActions where controller inherited from CustomControllerBase."
        );

        app.UseAdvanceMapControllers(areas);

        return app;
    }
}