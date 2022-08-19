using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.Channels;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ErrorLogTransmitter.Entities;
using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.Core.ErrorLogTransmitter.Producers;
using EasySoft.Core.GeneralLogTransmitter.Entities;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;
using EasySoft.Core.GeneralLogTransmitter.Producers;
using EasySoft.Core.Web.Framework.AccessControl;
using EasySoft.Core.Web.Framework.CommonAssists;
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

    /// <summary>
    /// 标记当前应用通道值, 用于远程日志等数据中, 便于数据辨认, 不使用此方法标记, 框架将采用内置值 0 代替
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="channel"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static WebApplicationBuilder UseAdvanceApplicationChannel(
        this WebApplicationBuilder builder,
        int channel
    )
    {
        if (FlagAssist.ApplicationChannelInjectionComplete)
        {
            throw new Exception("UseAdvanceApplicationChannel disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterInstance(new ApplicationChannel().SetChannel(channel))
                .As<IApplicationChannel>().SingleInstance();
        });

        FlagAssist.ApplicationChannelInjectionComplete = true;
        FlagAssist.ApplicationChannelIsDefault = false;

        return builder;
    }

    private static WebApplicationBuilder UseAdvanceDefaultApplicationChannel(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<ApplicationChannel>().As<IApplicationChannel>().SingleInstance();
        });

        FlagAssist.ApplicationChannelInjectionComplete = true;
        FlagAssist.ApplicationChannelIsDefault = true;

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
            builder.UseDefaultTokenSecretOptionsInjection();
        }

        if (!FlagAssist.TokenSecretInjectionComplete)
        {
            builder.UseDefaultTokenSecret();
        }

        if (!FlagAssist.ApplicationChannelInjectionComplete)
        {
            builder.UseAdvanceDefaultApplicationChannel();
        }

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

        if (FlagAssist.TokenSecretOptionIsDefault)
        {
            app.RecordWarning(
                "TokenSecretOption use DefaultTokenSecretOption, it is not safe, suggest using builder.UseTokenSecretOptionsInjection<T>() with your TokenSecretOption."
            );
        }

        if (FlagAssist.GetRemoteLogSwitch())
        {
            if (FlagAssist.ApplicationChannelIsDefault)
            {
                app.RecordInformation(
                    "ApplicationChannel use 0, suggest using builder.UseAdvanceApplicationChannel(int channel) with your Application, it make the data source easy to identify in the remote log."
                );
            }
            else
            {
                var applicationChannel = AutofacAssist.Instance.Container.Resolve<IApplicationChannel>();

                app.RecordInformation(
                    $"ApplicationChannel use {applicationChannel.GetChannel()}."
                );
            }
        }

        app.UseHttpsRedirection();

        if (GeneralConfigAssist.GetUseStaticFiles())
        {
            app.UseAdvanceStaticFiles();

            app.RecordInformation("useStaticFiles: enable"
            );
        }
        else
        {
            app.RecordInformation(
                "useStaticFiles: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        app.UseRouting();

        if (GeneralConfigAssist.GetCorsEnable())
        {
            app.UseCors(ConstCollection.DefaultSpecificOrigins);

            app.RecordInformation($"cors: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
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

        if (GeneralConfigAssist.GetRemoteGeneralLogEnable())
        {
            app.RecordInformation(
                "RemoteGeneralLogEnable: enable"
            );
        }
        else
        {
            app.RecordInformation(
                "RemoteGeneralLogEnable: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
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