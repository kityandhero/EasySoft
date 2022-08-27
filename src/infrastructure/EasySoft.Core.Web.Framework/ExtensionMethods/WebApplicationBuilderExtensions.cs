using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.ExtensionMethods;
using EasySoft.Core.EasyToken.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.ExtensionMethods;
using EasySoft.Core.Hangfire.ExtensionMethods;
using EasySoft.Core.PermissionVerification.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Filters;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Channels;
using EasySoft.Core.Infrastructure.Entities;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.JsonWebToken.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using EasySoft.Core.Swagger.ExtensionMethods;
using EasySoft.Core.Web.Framework.Attributes;
using EasySoft.Core.Web.Framework.Filters;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
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
        return EasyBuild(builder, areas, null);
    }

    public static WebApplication EasyBuild(
        this WebApplicationBuilder builder,
        Action<IEndpointRouteBuilder> endpointAction
    )
    {
        return EasyBuild(builder, new List<string>(), endpointAction);
    }

    public static WebApplication EasyBuild(
        this WebApplicationBuilder builder,
        List<string> areas,
        Action<IEndpointRouteBuilder>? endpointAction
    )
    {
        // AddMvc 最为全面， 涵盖 AddControllers 等的全部功能
        builder.Services.AddMvc(
                option =>
                {
                    option.EnableEndpointRouting = false;

                    if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.EasyToken &&
                        !FlagAssist.EasyTokenMiddlewareModeSwitch)
                    {
                        // 设置及接口数据返回格式
                        option.Filters.Add<EasyToken.Filters.OperatorFilter>();
                    }

                    if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.JsonWebToken &&
                        !FlagAssist.JsonWebTokenMiddlewareModeSwitch)
                    {
                        // 设置及接口数据返回格式
                        option.Filters.Add<JsonWebToken.Filters.OperatorFilter>();
                    }

                    if (FlagAssist.PermissionVerificationSwitch &&
                        !FlagAssist.PermissionVerificationMiddlewareModeSwitch)
                    {
                        // 设置及接口数据返回格式
                        option.Filters.Add<PermissionFilter>();
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

        LogAssist.SetLogger(app.Logger);

        EnvironmentAssist.SetEnvironment(app.Environment);

        AutofacAssist.Instance.Container = app.UseHostFiltering().ApplicationServices.GetAutofacRoot();

        ServiceAssist.ServiceProvider = app.Services;

        FlagAssist.SetApplicationRunPerformed();

        // 中间件调用顺序请参阅: https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message = UtilityTools.Standard.ConstCollection.Divider,
        });

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message = "application prepare to start, please wait a moment...."
        });

        var messageInfoList = new List<string>();

        if (GeneralConfigAssist.GetForwardedHeadersSwitch())
        {
            // 配置反向代理服务器, 需要在调用其他中间件之前 
            // https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-6.0
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = "ForwardedHeadersSwitch: enable."
            });
        }
        else
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message =
                    "ForwardedHeadersSwitch: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            });
        }

        app.UsePrepareStartWork();

        if (!app.Environment.IsDevelopment())
        {
            // app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message =
                $"CacheMode : {GeneralConfigAssist.GetCacheMode()}{(GeneralConfigAssist.GetCacheMode().Equals("redis", StringComparison.CurrentCultureIgnoreCase) ? $", Connections: {RedisConfigAssist.GetConnectionCollection().Join("|")}" : "")}, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
        });

        if (GeneralConfigAssist.GetRemoteLogSwitch())
        {
            if (FlagAssist.ApplicationChannelIsDefault)
            {
                StartupMessage.StartupMessageCollection.Add(new StartupMessage
                {
                    LogLevel = LogLevel.Information,
                    Message =
                        "ApplicationChannel use 0, suggest using builder.UseAdvanceApplicationChannel(int channel) with your Application, it make the data source easy to identify in the remote log."
                });
            }
            else
            {
                var applicationChannel = AutofacAssist.Instance.Container.Resolve<IApplicationChannel>();

                StartupMessage.StartupMessageCollection.Add(new StartupMessage
                {
                    LogLevel = LogLevel.Information,
                    Message = $"ApplicationChannel use {applicationChannel.GetChannel()}."
                });
            }
        }

        app.UseHttpsRedirection();

        if (GeneralConfigAssist.GetUseStaticFiles())
        {
            app.UseAdvanceStaticFiles();

            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = "useStaticFiles: enable."
            });
        }
        else
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message =
                    "useStaticFiles: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            });
        }

        app.UseRouting();

        if (GeneralConfigAssist.GetCorsSwitch())
        {
            app.UseCors(ConstCollection.DefaultSpecificOrigins);

            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = $"cors: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}."
            });
        }
        else
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message =
                    "cors: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            });
        }

        if (GeneralConfigAssist.GetUseAuthentication())
        {
            app.UseAuthentication();

            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message =
                    $"UseAuthentication: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}."
            });
        }
        else
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message =
                    "UseAuthentication: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            });
        }

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.EasyToken &&
            FlagAssist.EasyTokenMiddlewareModeSwitch)
        {
            // 设置及接口数据返回格式
            app.UseEasyTokenMiddleware();
        }

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.JsonWebToken &&
            FlagAssist.JsonWebTokenMiddlewareModeSwitch)
        {
            app.UseJsonWebTokenMiddleware();
        }

        if (!string.IsNullOrWhiteSpace(FlagAssist.TokenMode))
        {
            if (FlagAssist.EasyTokenMiddlewareModeSwitch || FlagAssist.JsonWebTokenMiddlewareModeSwitch)
            {
                StartupMessage.StartupMessageCollection.Add(new StartupMessage
                {
                    LogLevel = LogLevel.Information,
                    Message =
                        $"TokenMode: {FlagAssist.TokenMode}, use middleware mode, TokenServerDumpSwitch: {GeneralConfigAssist.GetTokenServerDumpSwitch()}, TokenParseFromUrlSwitch: {GeneralConfigAssist.GetTokenParseFromUrlSwitch()}, TokenParseFromCookieSwitch: {GeneralConfigAssist.GetTokenParseFromCookieSwitch()}."
                });
            }
            else
            {
                StartupMessage.StartupMessageCollection.Add(new StartupMessage
                {
                    LogLevel = LogLevel.Information,
                    Message =
                        $"TokenMode: {FlagAssist.TokenMode}, use filter mode, TokenServerDumpSwitch: {GeneralConfigAssist.GetTokenServerDumpSwitch()}, TokenParseFromUrlSwitch: {GeneralConfigAssist.GetTokenParseFromUrlSwitch()}, TokenParseFromCookieSwitch: {GeneralConfigAssist.GetTokenParseFromCookieSwitch()}."
                });
            }
        }

        if (FlagAssist.PermissionVerificationSwitch)
        {
            if (string.IsNullOrWhiteSpace(FlagAssist.TokenMode))
            {
                throw new Exception("use PermissionVerification need config one of token mode");
            }

            if (FlagAssist.PermissionVerificationMiddlewareModeSwitch)
            {
                app.UsePermissionVerificationMiddleware();
            }

            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = FlagAssist.PermissionVerificationMiddlewareModeSwitch
                    ? "PermissionVerificationSwitch: enable, use middleware mode."
                    : "PermissionVerificationSwitch: enable, use filter mode."
            });
        }

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message = GeneralConfigAssist.GetAccessWayDetectSwitch()
                ? $"AccessWayDetectSwitch: enable."
                : "AccessWayDetectSwitch: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
        });

        if (GeneralConfigAssist.GetUseAuthorization())
        {
            app.UseAuthorization();

            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = $"UseAuthorization: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}."
            });
        }
        else
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message =
                    "UseAuthorization: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            });
        }

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message = GeneralConfigAssist.GetRemoteGeneralLogSwitch()
                ? "RemoteGeneralLogEnable: enable."
                : "RemoteGeneralLogEnable: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
        });

        app.UseAdvanceSwagger();

        app.UseAdvanceHangfire();

        messageInfoList.Add(
        );

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message =
                "you can set your autoFac config with autoFac.json in ./configures/autoFac.json. The document link is https://autofac.readthedocs.io/en/latest/configuration/xml.html."
        });

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message =
                "you can get all controller actions by visit https://[host]:[port]/[controller]/getAllActions where controller inherited from CustomControllerBase."
        });

        app.UseAdvanceMapControllers(areas, endpointAction);

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message = GeneralConfigAssist.GetAgileConfigSwitch()
                ? "AgileConfigSwitch: enable."
                : "AgileConfigSwitch: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
        });

        if (GeneralConfigAssist.GetAgileConfigSwitch())
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message =
                    $"dynamic config key: {Config.ConstCollection.GetDynamicConfigKeyCollection().Join(",")}, they can set in AgileConfig."
            });
        }

        if (FlagAssist.HealthChecksSwitch)
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = $"HealthChecks: enable{(string.IsNullOrWhiteSpace(FlagAssist.StartupUrls)?".":$", you can access {FlagAssist.StartupUrls}")}/healthchecks-ui to visit it.",
            });
        }

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message = $"application start completed, please access {app.Urls.Join(",")}",
        });

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message = UtilityTools.Standard.ConstCollection.Divider
        });

        StartupMessage.Print();

        return app;
    }
}