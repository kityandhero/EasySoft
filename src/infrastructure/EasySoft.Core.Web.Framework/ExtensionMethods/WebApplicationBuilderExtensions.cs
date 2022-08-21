using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.ExtensionMethods;
using EasySoft.Core.Hangfire.ExtensionMethods;
using EasySoft.Core.IdentityVerification.ExtensionMethods;
using EasySoft.Core.IdentityVerification.Filters;
using EasySoft.Core.IdentityVerification.Middlewares;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Channels;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using EasySoft.Core.Swagger.ExtensionMethods;
using EasySoft.Core.Web.Framework.Attributes;
using EasySoft.Core.Web.Framework.Filters;
using EasySoft.UtilityTools.ExtensionMethods;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplication EasyBuild(this WebApplicationBuilder builder)
    {
        return EasyBuild(builder, new List<string>());
    }

    public static WebApplication EasyBuild(this WebApplicationBuilder builder, List<string> areas)
    {
        // AddMvc 最为全面， 涵盖 AddControllers 等的全部功能
        builder.Services.AddMvc(
                option =>
                {
                    option.EnableEndpointRouting = false;

                    if (FlagAssist.IdentityVerificationSwitch && !FlagAssist.IdentityVerificationMiddlewareModeSwitch)
                    {
                        // 设置及接口数据返回格式
                        option.Filters.Add<OperatorFilter>();
                    }

                    // 设置及接口数据返回格式
                    option.Filters.Add<WebApiResultFilterAttribute>();

                    // 设置全局异常过滤器
                    option.Filters.Add<GlobalExceptionFilter>();
                }
            )
            // 爆露ApplicationPartManager 实例给外部工具，用以实现某些特定功能
            .ConfigureApplicationPartManager(ApplicationPartManagerAssist.SetApplicationPartManager)
            // 通过AddControllersAsServices方法，将控制器交给 autofac 容器来处理，可以使“属性注入”
            .AddControllersAsServices()
            .AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver(); //序列化时key为驼峰样式
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //忽略循环引用
                }
            );

        // builder.Services.AddSingleton<IHostEnvironment, HostingEnvironment>();

        // 扩展支持此类使用 @Html.Action("UserBackView", "UserManage")  
        // builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        // builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<HostingEnvironment>().As<IHostEnvironment>().SingleInstance();
            containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            containerBuilder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().SingleInstance();
        });

        if (GeneralConfigAssist.GetCorsSwitch())
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: ConstCollection.DefaultSpecificOrigins,
                    configPolicy => { configPolicy.WithOrigins(GeneralConfigAssist.GetCorsPolicies().ToArray()); }
                );
            });
        }

        if (SwaggerConfigAssist.GetEnable())
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        builder.UseAdvanceEasyCaching();

        builder.UseGeneralLogTransmitter();

        builder.UseErrorLogTransmitter();

        if (HangfireConfigAssist.GetEnable())
        {
            var storage = HangfireConfigAssist.GetStorage();

            switch (storage)
            {
                case "MemoryStorage":
                    builder.Services.AddHangfire(x => x.UseStorage(new MemoryStorage()));
                    break;

                default:
                    throw new Exception($"Hangfire config Storage {storage} does not support just");
            }

            //启用Hangfire服务.
            builder.Services.AddHangfireServer();
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

        app.RecordInformation(
            $"CacheMode : {GeneralConfigAssist.GetCacheMode()}{(GeneralConfigAssist.GetCacheMode().Equals("redis", StringComparison.CurrentCultureIgnoreCase) ? $", Connections: {RedisConfigAssist.GetConnectionCollection().Join("|")}" : "")}, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json.."
        );

        if (GeneralConfigAssist.GetRemoteLogSwitch())
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

        if (GeneralConfigAssist.GetCorsSwitch())
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

        if (FlagAssist.IdentityVerificationSwitch && FlagAssist.IdentityVerificationMiddlewareModeSwitch)
        {
            // 设置及接口数据返回格式
            app.UseIdentityVerificationMiddleware();
        }

        if (FlagAssist.IdentityVerificationSwitch)
        {
            app.RecordInformation(
                $"IdentityVerificationSwitch: enable"
            );
        }

        if (GeneralConfigAssist.GetAccessWayDetectSwitch())
        {
            app.RecordInformation(
                $"AccessWayDetectSwitch: enable"
            );
        }
        else
        {
            app.RecordInformation(
                "AccessWayDetectSwitch: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
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

        if (GeneralConfigAssist.GetRemoteGeneralLogSwitch())
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