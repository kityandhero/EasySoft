using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Cap.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Options;
using EasySoft.Core.DevelopAuxiliary.ExtensionMethods;
using EasySoft.Core.EasyCaching.ExtensionMethods;
using EasySoft.Core.EasyToken.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.ExtensionMethods;
using EasySoft.Core.Hangfire.ExtensionMethods;
using EasySoft.Core.PermissionVerification.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Filters;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.JsonWebToken.ExtensionMethods;
using EasySoft.Core.MiniProfiler.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using EasySoft.Core.Swagger.ExtensionMethods;
using EasySoft.Core.Web.Framework.Attributes;
using EasySoft.Core.Web.Framework.Filters;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplication EasyBuild(
        this WebApplicationBuilder builder
    )
    {
        WeaveApplicationBuilderExtraAction(builder);

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

                    WeaveMvcOptionExtraAction(option);
                }
            )
            // 爆露ApplicationPartManager 实例给外部工具，用以实现某些特定功能
            .ConfigureApplicationPartManager(ApplicationPartManagerAssist.SetApplicationPartManager)
            // 通过AddControllersAsServices方法，将控制器交给 autofac 容器来处理，可以使“属性注入”
            .AddControllersAsServices()
            .AddNewtonsoftJson(
                options =>
                {
                    //序列化时key为驼峰样式
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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

        builder.AddAdvanceEasyCaching();

        builder.AddAdvanceMiniProfile();

        builder.AddGeneralLogTransmitter();

        builder.AddErrorLogTransmitter();

        builder.AddAdvanceCap();

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
            builder.AddAdvanceDefaultApplicationChannel();
        }

        var app = builder.Build();

        LogAssist.SetLogger(app.Logger);

        EnvironmentAssist.SetEnvironment(app.Environment);

        AutofacAssist.Instance.SetContainer(app.UseHostFiltering().ApplicationServices.GetAutofacRoot());

        ServiceAssist.SetServiceProvider(app.Services);

        FlagAssist.SetApplicationRunPerformed();

        // 中间件调用顺序请参阅: https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order

        if (GeneralConfigAssist.GetForwardedHeadersSwitch())
        {
            // 配置反向代理服务器, 需要在调用其他中间件之前 
            // https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-6.0
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "ForwardedHeadersSwitch: enable."
                    )
                    .SetExtra(RedisConfigAssist.GetConfigFileInfo())
            );
        }
        else
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "ForwardedHeadersSwitch: disable."
                    )
                    .SetExtra(RedisConfigAssist.GetConfigFileInfo())
            );
        }

        app.UsePrepareStartWork();

        if (!app.Environment.IsDevelopment())
        {
            // app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"CacheMode : {GeneralConfigAssist.GetCacheMode()}."
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        if (GeneralConfigAssist.GetCacheMode() == CacheModeCollection.Redis.ToString())
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"{CacheModeCollection.Redis.ToString()} Connections: {RedisConfigAssist.GetConnectionCollection().Join("|")}."
                    )
                    .SetExtra(RedisConfigAssist.GetConfigFileInfo())
            );
        }

        if (GeneralConfigAssist.GetRemoteLogSwitch())
        {
            if (FlagAssist.ApplicationChannelIsDefault)
            {
                StartupConfigMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            "ApplicationChannel use 0, suggest using builder.UseAdvanceApplicationChannel(int channel) with your Application, it make the data source easy to identify in the remote log."
                        )
                );
            }
            else
            {
                var applicationChannel = AutofacAssist.Instance.Resolve<IApplicationChannel>();

                StartupConfigMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"ApplicationChannel use {applicationChannel.GetChannel()}."
                        )
                );
            }
        }

        if (GeneralConfigAssist.GetHttpRedirectionHttpsSwitch())
        {
            // 当前项目启动后，监听的是否是多个端口，其中如果有协议是Https—我们在访问Http的默认会转发到Https中
            app.UseHttpsRedirection();

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "HttpRedirectionHttpsSwitch: enable."
                    )
            );
        }
        else
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "HttpRedirectionHttpsSwitch: disabled."
                    )
            );
        }

        if (GeneralConfigAssist.GetUseStaticFilesSwitch())
        {
            app.UseAdvanceStaticFiles();

            var staticFileOptionsTypeName = "";

            if (FlagAssist.GetAdvanceStaticFileOptionsSwitch())
            {
                staticFileOptionsTypeName = AutofacAssist.Instance.Resolve<AdvanceStaticFileOptions>().GetType().Name;
            }

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"UseStaticFilesSwitch: enable, mode: {(FlagAssist.GetAdvanceStaticFileOptionsSwitch() ? $"custom, config class is \"{staticFileOptionsTypeName}\"" : "default")}."
                    )
            );
        }
        else
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "UseStaticFiles: disable."
                    ).SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );
        }

        app.UseRouting();

        if (GeneralConfigAssist.GetCorsSwitch())
        {
            app.UseCors(ConstCollection.DefaultSpecificOrigins);

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"cors: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}."
                    )
            );
        }
        else
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "Cors: disable."
                    )
                    .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );
        }

        if (GeneralConfigAssist.GetUseAuthentication())
        {
            app.UseAuthentication();

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"UseAuthentication: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}."
                    )
            );
        }
        else
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "UseAuthentication: disable."
                    )
                    .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );
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
                StartupConfigMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"TokenMode: {FlagAssist.TokenMode}, use middleware mode, TokenServerDumpSwitch: {GeneralConfigAssist.GetTokenServerDumpSwitch()}, TokenParseFromUrlSwitch: {GeneralConfigAssist.GetTokenParseFromUrlSwitch()}, TokenParseFromCookieSwitch: {GeneralConfigAssist.GetTokenParseFromCookieSwitch()}."
                        )
                );
            }
            else
            {
                StartupConfigMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"TokenMode: {FlagAssist.TokenMode}, use filter mode, TokenServerDumpSwitch: {GeneralConfigAssist.GetTokenServerDumpSwitch()}, TokenParseFromUrlSwitch: {GeneralConfigAssist.GetTokenParseFromUrlSwitch()}, TokenParseFromCookieSwitch: {GeneralConfigAssist.GetTokenParseFromCookieSwitch()}."
                        )
                );
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

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        FlagAssist.PermissionVerificationMiddlewareModeSwitch
                            ? "PermissionVerificationSwitch: enable, use middleware mode."
                            : "PermissionVerificationSwitch: enable, use filter mode."
                    )
            );
        }

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    GeneralConfigAssist.GetAccessWayDetectSwitch()
                        ? "AccessWayDetectSwitch: enable."
                        : "AccessWayDetectSwitch: disable."
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        if (GeneralConfigAssist.GetUseAuthorization())
        {
            app.UseAuthorization();

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"UseAuthorization: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}."
                    )
            );
        }
        else
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "UseAuthorization: disable."
                    )
                    .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );
        }

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    GeneralConfigAssist.GetRemoteErrorLogSwitch()
                        ? "RemoteErrorLogEnable: enable."
                        : "RemoteErrorLogEnable: disable."
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    GeneralConfigAssist.GetRemoteGeneralLogSwitch()
                        ? "RemoteGeneralLogEnable: enable."
                        : "RemoteGeneralLogEnable: disable."
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseDevelopAuxiliary();
        }

        WeaveApplicationExtraAction(app);

        app.UseAdvanceSwagger();

        app.UseAdvanceHangfire();

        app.UseAdvanceMapControllers();

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"Environment: {EnvironmentAssist.GetEnvironment().EnvironmentName}."
                )
        );

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"ContentRootPath: \"{EnvironmentAssist.GetEnvironment().ContentRootPath}\"."
                )
        );

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"WebRootPath: \"{GetWebRootPath()}\"."
                )
        );

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"Application start completed{(!FlagAssist.StartupUrls.Any() ? "." : $" at {FlagAssist.StartupUrls.Join(" ")}.")}"
                )
        );

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    UtilityTools.Standard.ConstCollection.ApplicationStartMessageDivider
                )
        );

        StartupBuilderExtraActionMessageAssist.Print();

        StartupApplicationExtraActionMessageAssist.Print();

        StartupMvcOptionExtraActionMessageAssist.Print();

        StartupEndPointExtraActionMessageAssist.Print();

        StartupConfigMessageAssist.Print();

        StartupDescriptionMessageAssist.Print();

        return app;
    }

    private static string GetWebRootPath()
    {
        var result = EnvironmentAssist.GetEnvironment().WebRootPath;

        if (!FlagAssist.GetAdvanceStaticFileOptionsSwitch())
        {
            return result;
        }

        if (!FlagAssist.GetAdvanceStaticFileOptionsSwitch())
        {
            return result;
        }

        return string.IsNullOrWhiteSpace(GeneralConfigAssist.GetWebRootPath())
            ? result
            : EnvironmentAssist.GetEnvironment().ContentRootPath.Combine(GeneralConfigAssist.GetWebRootPath());
    }

    /// <summary>
    /// 织入扩展的 Application Action
    /// </summary>
    private static void WeaveApplicationBuilderExtraAction(
        WebApplicationBuilder builder
    )
    {
        var extraActions = ApplicationConfigurator.GetAllWebApplicationBuilderExtraActions().ToList();

        if (extraActions.Count <= 0)
        {
            return;
        }

        var startMessage = new StartupMessage()
            .SetLevel(LogLevel.Information)
            .SetMessage(
                UtilityTools.Standard.ConstCollection.ApplicationStartExtraBuilderMessageDivider
            );

        StartupBuilderExtraActionMessageAssist.Add(startMessage);

        var i = 1;

        extraActions.ForEach(extraAction =>
        {
            var action = extraAction.GetAction();

            if (action == null)
            {
                return;
            }

            var name = extraAction.GetName();

            if (!string.IsNullOrWhiteSpace(name))
            {
                StartupBuilderExtraActionMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"{i}: {name}"
                        )
                );

                i += 1;
            }

            action(builder);
        });
    }

    /// <summary>
    /// 织入扩展的 Application Action
    /// </summary>
    private static void WeaveApplicationExtraAction(
        WebApplication application
    )
    {
        var extraActions = ApplicationConfigurator.GetAllWebApplicationExtraActions().ToList();

        if (extraActions.Count <= 0)
        {
            return;
        }

        var startMessage = new StartupMessage()
            .SetLevel(LogLevel.Information)
            .SetMessage(
                UtilityTools.Standard.ConstCollection.ApplicationStartExtraApplicationMessageStartDivider
            );

        StartupApplicationExtraActionMessageAssist.Add(startMessage);

        var i = 1;

        extraActions.ForEach(extraAction =>
        {
            var action = extraAction.GetAction();

            if (action == null)
            {
                return;
            }

            var name = extraAction.GetName();

            if (!string.IsNullOrWhiteSpace(name))
            {
                StartupApplicationExtraActionMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"{i}: {name}"
                        )
                );

                i += 1;
            }

            action(application);
        });
    }

    /// <summary>
    /// 织入扩展的 MvcOptions Action
    /// </summary>
    private static void WeaveMvcOptionExtraAction(
        MvcOptions option
    )
    {
        var extraActions = ApplicationConfigurator.GetAllMvcOptionExtraActions().ToList();

        if (extraActions.Count <= 0)
        {
            return;
        }

        var startMessage = new StartupMessage()
            .SetLevel(LogLevel.Information)
            .SetMessage(
                UtilityTools.Standard.ConstCollection.ApplicationStartExtraMvcOptionMessageStartDivider
            );

        StartupMvcOptionExtraActionMessageAssist.Add(startMessage);

        var i = 1;

        extraActions.ForEach(extraAction =>
        {
            var action = extraAction.GetAction();

            if (action == null)
            {
                return;
            }

            var name = extraAction.GetName();

            if (!string.IsNullOrWhiteSpace(name))
            {
                StartupMvcOptionExtraActionMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"{i}: {name}"
                        )
                );

                i += 1;
            }

            action(option);
        });
    }
}