using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Web.Framework.AccessControl;
using EasySoft.Core.Web.Framework.CommonAssists;
using EasySoft.Core.Web.Framework.IocAssists;
using EasySoft.Core.Web.Framework.PrepareWorks;
using EasySoft.Core.Web.Framework.Selectors;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseAdvanceAutoFac(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(AutofacAssist.Init);

        UseCovertInjection(builder);

        UseControllerPropertiesAutowired(builder, Assembly.GetEntryAssembly());

        return builder;
    }

    /// <summary>
    /// 注入框架的启动预处理任务，启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder UseCovertInjection(
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
    public static WebApplicationBuilder UsePrepareStartWorkInjection<T>(
        this WebApplicationBuilder builder
    ) where T : IPrepareStartWork
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<IPrepareStartWork>().SingleInstance();
        });

        return builder;
    }

    /// <summary>
    /// 注入定制的静态文件配置，诸如任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder UseStaticFileOptionsInjection<T>(
        this WebApplicationBuilder builder
    ) where T : StaticFileOptions
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<StaticFileOptions>().SingleInstance();
        });

        return builder;
    }

    /// <summary>
    /// 注入Token密钥配置, 未配置此项将使用内置密钥, 但默认密钥不安全的
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder UseTokenSecretOptionsInjection<T>(
        this WebApplicationBuilder builder
    ) where T : ITokenSecretOptions
    {
        if (FlagAssist.TokenSecretOptionInjectionComplete)
        {
            throw new Exception("UseTokenSecretOptionsInjection<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<ITokenSecretOptions>().SingleInstance();
        });

        FlagAssist.TokenSecretOptionInjectionComplete = true;
        FlagAssist.TokenSecretOptionIsDefault = false;

        return builder;
    }

    /// <summary>
    /// 注入Token加解密工具, 未配置此项将使用内置工具
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder UseTokenSecretInjection<T>(
        this WebApplicationBuilder builder
    ) where T : ITokenSecret
    {
        if (FlagAssist.TokenSecretInjectionComplete)
        {
            throw new Exception("UseTokenSecretInjection<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<ITokenSecret>().SingleInstance();
        });

        FlagAssist.TokenSecretInjectionComplete = true;

        return builder;
    }

    /// <summary>
    /// 注入Token密钥配置
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder UseDefaultTokenSecretOptionsInjection(
        this WebApplicationBuilder builder
    )
    {
        if (FlagAssist.TokenSecretOptionInjectionComplete)
        {
            throw new Exception("UseTokenSecretOptionsInjection<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<DefaultTokenSecretOptions>().As<ITokenSecretOptions>().SingleInstance();
        });

        FlagAssist.TokenSecretOptionInjectionComplete = true;
        FlagAssist.TokenSecretOptionIsDefault = true;

        return builder;
    }

    /// <summary>
    /// 注入默认Token密钥配置
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder UseDefaultTokenSecret(
        this WebApplicationBuilder builder
    )
    {
        if (FlagAssist.TokenSecretInjectionComplete)
        {
            throw new Exception("UseDefaultTokenSecret<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<TokenSecret>().As<ITokenSecret>().SingleInstance();
        });

        return builder;
    }

    public static WebApplicationBuilder UseExtraNormalInjection(
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

    public static WebApplicationBuilder UseControllerPropertiesAutowired(
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
        return EasyBuild(builder, new List<string>());
    }

    public static WebApplication EasyBuild(this WebApplicationBuilder builder, List<string> areas)
    {
        if (!FlagAssist.TokenSecretOptionInjectionComplete)
        {
            UseDefaultTokenSecretOptionsInjection(builder);
        }

        if (!FlagAssist.TokenSecretInjectionComplete)
        {
            UseDefaultTokenSecret(builder);
        }

        var app = builder.Build();

        AutofacAssist.Instance.Container = app.UseHostFiltering().ApplicationServices.GetAutofacRoot();

        ServiceAssist.ServiceProvider = app.Services;

        // 中间件调用顺序请参阅: https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order

        WebApplicationExtensions.UsePrepareStartWork(app);

        if (!app.Environment.IsDevelopment())
        {
            // app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        if (FlagAssist.TokenSecretOptionIsDefault)
        {
            WebApplicationExtensions.RecordWarning(app, "TokenSecretOption use DefaultTokenSecretOption, it is not safe, suggest using builder.UseTokenSecretOptionsInjection<T>() with your TokenSecretOption."
            );
        }

        app.UseHttpsRedirection();

        if (GeneralConfigAssist.GetUseStaticFiles())
        {
            WebApplicationExtensions.UseAdvanceStaticFiles(app);

            WebApplicationExtensions.RecordInformation(app, $"useStaticFiles: enable"
            );
        }
        else
        {
            WebApplicationExtensions.RecordInformation(app, "useStaticFiles: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        app.UseRouting();

        if (GeneralConfigAssist.GetCorsEnable())
        {
            app.UseCors(ConstCollection.DefaultSpecificOrigins);

            WebApplicationExtensions.RecordInformation(app, $"cors: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            WebApplicationExtensions.RecordInformation(app, "cors: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        if (GeneralConfigAssist.GetUseAuthentication())
        {
            app.UseAuthentication();

            WebApplicationExtensions.RecordInformation(app, $"UseAuthentication: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            WebApplicationExtensions.RecordInformation(app, "UseAuthentication: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        if (GeneralConfigAssist.GetUseAuthorization())
        {
            app.UseAuthorization();

            WebApplicationExtensions.RecordInformation(app, $"UseAuthorization: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            WebApplicationExtensions.RecordInformation(app, "UseAuthorization: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        WebApplicationExtensions.UseAdvanceSwagger(app);

        WebApplicationExtensions.UseAdvanceHangfire(app);

        WebApplicationExtensions.RecordInformation(app, "you can set your autoFac config with autoFac.json in ./configures/autoFac.json. The document link is https://autofac.readthedocs.io/en/latest/configuration/xml.html."
        );

        WebApplicationExtensions.RecordInformation(app, "you can get all controller actions by visit https://[host]:[port]/[controller]/getAllActions where controller inherited from CustomControllerBase."
        );

        WebApplicationExtensions.UseAdvanceMapControllers(app, areas);

        return app;
    }
}