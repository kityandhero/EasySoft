using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.DependencyInjection.IocAssists;
using EasySoft.Core.EntityFramework.Extensions;
using EasySoft.Core.GeneralLogTransceiver.Extensions;
using EasySoft.Core.Infrastructure.Extensions;
using EasySoft.Core.Infrastructure.Options;
using EasySoft.Core.PrepareStartWork.PrepareWorks;
using EasySoft.Core.Swagger.Configures;
using EasySoft.Core.Swagger.ExtensionMethods;
using EasySoft.Core.Web.Framework.ActionModelConventions;
using EasySoft.Core.Web.Framework.Attributes;
using EasySoft.Core.Web.Framework.Filters;
using IChannel = EasySoft.UtilityTools.Standard.Interfaces.IChannel;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddCovertInjection = "1CE0086F-567F-4838-9611-55819A9A549B";

    // /// <summary>
    // /// 注入框架的启动预处理任务，启动时自动执行, 仅允许配置一次
    // /// </summary>
    // /// <param name="builder"></param>
    // /// <returns></returns>
    // public static WebApplicationBuilder AddCovertInjection(
    //     this WebApplicationBuilder builder
    // )
    // {
    //     if (builder.HasRegistered(UniqueIdentifierAddCovertInjection))
    //     {
    //         return builder;
    //     }
    //
    //     builder.Services.AddSingleton<IPrepareCovertStartWork, PrepareCovertStartWork>();
    //
    //     return builder;
    // }

    public static WebApplication EasyBuild(
        this WebApplicationBuilder builder
    )
    {
        // 核心集成
        // builder.Services.AddCacheCore();

        // builder.AddAdvanceAutoFac();
        // builder.AddCovertInjection();

        // 核心集成
        // builder.AddAdvanceMapster();

        // builder.AddRegistrationCenter();

        // builder.AddConfigCenter(channel);
        // builder.AddGateway();
        builder.AddAdvanceExceptionless();

        // 核心集成
        // if (GeneralConfigAssist.GetCompressionSwitch())
        // {
        //     StartupConfigMessageAssist.AddConfig(
        //         "CompressionSwitch: enable.",
        //         GeneralConfigAssist.GetConfigFileInfo()
        //     );
        //
        //     builder.Services.AddAdvanceResponseCompression();
        // }
        // else
        // {
        //     StartupConfigMessageAssist.AddConfig(
        //         "CompressionSwitch: disable.",
        //         GeneralConfigAssist.GetConfigFileInfo()
        //     );
        // }

        // 核心集成
        // builder.Services.AddRouting(o => { o.LowercaseUrls = true; });

        // 核心集成
        // builder.Services.AddApiVersioning(o =>
        //     {
        //         //return versions in a response header
        //         o.ReportApiVersions = true;
        //
        //         //default version select 
        //         o.DefaultApiVersion = new ApiVersion(1, 0);
        //
        //         //if not specifying an api version,show the default version
        //         o.AssumeDefaultVersionWhenUnspecified = true;
        //     }
        // );

        // builder.Services.AddSingleton<IHostEnvironment, HostingEnvironment>();

        // 扩展支持此类使用 @Html.Action("UserBackView", "UserManage")  
        // builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        // builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        // 核心集成
        // builder.Services.AddSingleton<IHostEnvironment, HostingEnvironment>();
        // builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        // builder.Services.AddHttpContextAccessor();

        // 核心集成 
        // builder.Services.AddHttpClient();

        // 核心集成
        // if (GeneralConfigAssist.GetCorsSwitch())
        // {
        //     builder.Services.AddCors(options =>
        //         {
        //             options.AddPolicy(
        //                 ConstCollection.DefaultSpecificOrigins,
        //                 configPolicy =>
        //                 {
        //                     configPolicy.WithOrigins(GeneralConfigAssist.GetCorsPolicies().ToArray());
        //                     configPolicy.AllowAnyHeader();
        //                     configPolicy.AllowAnyMethod();
        //                     configPolicy.AllowCredentials();
        //                 }
        //             );
        //         }
        //     );
        // }

        // builder.Services.AddEndpointsApiExplorer();

        builder.AddLogRecordInterceptor();

        builder.AddAdvanceSwagger();

        builder.AddAdvanceMediatR();

        // 核心集成
        // builder.AddAdvanceCacheImplementation();

        // 核心集成
        // builder.AddAdvanceMiniProfile();

        builder.AddGeneralLogRabbitMQOnlyProducer();

        builder.AddErrorLogRabbitMQOnlyProducer();

        builder.AddSqlLogRabbitMQOnlyProducer();

        // builder.AddAdvanceCap();

        builder.AddAdvanceHangfire();

        builder.AddAdvanceEntityFrameworkCore();

        builder.AddAssemblyBusinessServices();

        var app = builder.Build();

        // LogAssist.SetLogger(app.Logger);

        // 核心集成
        // ServiceProviderAssist.SetProvider(app.Services);

        // 核心集成
        // FlagAssist.SetApplicationRunPerformed();

        // 中间件调用顺序请参阅: https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order

        // ASP.NET Core MVC的过滤器类别以及默认执行顺序：
        // https://www.cnblogs.com/yangxu-pro/p/9010978.html
        // 授权过滤器，它是第一个运行的，它的作用就是判断HTTP Context中的用户是否拥有当前请求的权限，如果用户没有权限，那么它就会“短路”管道。
        // 资源过滤器，在授权过滤器后运行，在管道其它动作之前，和管道动作都结束后运行。它可以实现缓存或由于性能原因执行短路操作。它在实体绑定之前运行，所以它也可以对影响实体绑定。
        // Action过滤器，它在Action方法调用之前和之后立即执行，它可以操作传进Action的参数和返回的结果。
        // 异常过滤器，针对在写入响应Body之前发生的未处理的异常，它可以应用全局的策略，
        // 结果过滤器，它可以在每个Action结果执行之前和之后运行代码，但也只是在Action方法无错误的成功完成后才可以执行。

        // 核心集成
        // if (GeneralConfigAssist.GetForwardedHeadersSwitch())
        // {
        //     // 配置反向代理服务器, 需要在调用其他中间件之前 
        //     // https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-6.0
        //     // https: //learn.microsoft.com/zh-cn/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-6.0
        //     app.UseForwardedHeaders(
        //         new ForwardedHeadersOptions
        //         {
        //             ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        //         }
        //     );
        //
        //     StartupConfigMessageAssist.AddConfig(
        //         "ForwardedHeadersSwitch: enable.",
        //         GeneralConfigAssist.GetConfigFileInfo()
        //     );
        // }
        // else
        // {
        //     StartupConfigMessageAssist.AddConfig(
        //         "ForwardedHeadersSwitch: disable.",
        //         GeneralConfigAssist.GetConfigFileInfo()
        //     );
        // }

        // app.UsePrepareStartWork();

        // 核心集成
        // if (!app.Environment.IsDevelopment())
        //
        //     // app.UseExceptionHandler("/Error");
        // {
        //     if (GeneralConfigAssist.GetHstsSwitch())
        //     {
        //         app.UseHsts();
        //     }
        // }

        // 核心集成
        // if (GeneralConfigAssist.GetHttpRedirectionHttpsSwitch())
        // {
        //     // 当前项目启动后，监听的是否是多个端口，其中如果有协议是Https—我们在访问Http的默认会转发到Https中
        //     app.UseHttpsRedirection();
        //
        //     StartupConfigMessageAssist.AddConfig(
        //         "HttpRedirectionHttpsSwitch: enable.",
        //         GeneralConfigAssist.GetConfigFileInfo()
        //     );
        // }
        // else
        // {
        //     StartupConfigMessageAssist.AddConfig(
        //         "HttpRedirectionHttpsSwitch: disabled.",
        //         GeneralConfigAssist.GetConfigFileInfo()
        //     );
        // }

        if (GeneralConfigAssist.GetUseStaticFilesSwitch())
        {
            app.UseAdvanceStaticFiles();

            var staticFileOptionsTypeName = "";

            if (FlagAssist.GetAdvanceStaticFileOptionsSwitch())
            {
                var staticFileOptions = app.Services.GetService<AdvanceStaticFileOptions>();

                if (staticFileOptions != null)
                {
                    staticFileOptionsTypeName = staticFileOptions.GetType().Name;
                }
            }

            StartupConfigMessageAssist.AddConfig(
                $"UseStaticFilesSwitch: enable, mode: {(FlagAssist.GetAdvanceStaticFileOptionsSwitch() ? $"custom, config class is \"{staticFileOptionsTypeName}\"" : "default")}.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }
        else
        {
            StartupConfigMessageAssist.AddConfig(
                "UseStaticFiles: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }

        // app.UseRouting();

        if (GeneralConfigAssist.GetCorsSwitch())
        {
            app.UseCors(ConstCollection.DefaultSpecificOrigins);

            StartupConfigMessageAssist.AddConfig(
                $"cors: enable, policies: {GeneralConfigAssist.GetCorsPolicies().Join(",")}."
            );
        }
        else
        {
            StartupConfigMessageAssist.AddConfig(
                "Cors: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }

        if (GeneralConfigAssist.GetUseAuthentication())
        {
            app.UseAuthentication();

            StartupConfigMessageAssist.AddConfig(
                $"UseAuthentication: enable, policies: {GeneralConfigAssist.GetCorsPolicies().Join(",")}."
            );
        }
        else
        {
            StartupConfigMessageAssist.AddConfig(
                "UseAuthentication: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.EasyToken &&
            FlagAssist.EasyTokenMiddlewareModeSwitch)

            // 设置及接口数据返回格式
        {
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
                StartupConfigMessageAssist.AddConfig(
                    $"TokenMode: {FlagAssist.TokenMode}, use middleware mode, TokenServerDumpSwitch: {GeneralConfigAssist.GetTokenServerDumpSwitch()}, TokenParseFromUrlSwitch: {GeneralConfigAssist.GetTokenParseFromUrlSwitch()}, TokenParseFromCookieSwitch: {GeneralConfigAssist.GetTokenParseFromCookieSwitch()}.",
                    GeneralConfigAssist.GetConfigFileInfo()
                );
            }
            else
            {
                StartupConfigMessageAssist.AddConfig(
                    $"TokenMode: {FlagAssist.TokenMode}, use filter mode, TokenServerDumpSwitch: {GeneralConfigAssist.GetTokenServerDumpSwitch()}, TokenParseFromUrlSwitch: {GeneralConfigAssist.GetTokenParseFromUrlSwitch()}, TokenParseFromCookieSwitch: {GeneralConfigAssist.GetTokenParseFromCookieSwitch()}.",
                    GeneralConfigAssist.GetConfigFileInfo()
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

            StartupConfigMessageAssist.AddConfig(
                FlagAssist.PermissionVerificationMiddlewareModeSwitch
                    ? "PermissionVerificationSwitch: enable, use middleware mode."
                    : "PermissionVerificationSwitch: enable, use filter mode."
            );
        }

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetAccessWayDetectSwitch()
                ? "AccessWayDetectSwitch: enable."
                : "AccessWayDetectSwitch: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        if (GeneralConfigAssist.GetUseAuthorization())
        {
            app.UseAuthorization();

            StartupConfigMessageAssist.AddConfig(
                $"UseAuthorization: enable, policies: {GeneralConfigAssist.GetCorsPolicies().Join(",")}."
            );
        }
        else
        {
            StartupConfigMessageAssist.AddConfig(
                "UseAuthorization: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAdvanceEnvironmentAuxiliary();

        app.UseConfigureTemplate();

        StartupConfigMessageAssist.AddConfig(
            $"Environment: {EnvironmentAssist.GetEnvironment().EnvironmentName}."
        );

        StartupConfigMessageAssist.AddConfig(
            $"EnvironmentAlias: {EnvironmentAssist.GetEnvironmentAliasName()}."
        );

        StartupConfigMessageAssist.AddConfig(
            $"ContentRootPath: \"{EnvironmentAssist.GetEnvironment().ContentRootPath}\"."
        );

        StartupConfigMessageAssist.AddConfig(
            $"WebRootPath: \"{GetWebRootPath()}\"."
        );

        StartupConfigMessageAssist.AddConfig(
            $"Application start completed{(!FlagAssist.StartupUrls.Any() ? "." : $" at {FlagAssist.StartupUrls.Join(" ")}.")}"
        );

        // StartupBuilderExtraActionMessageAssist.Print();
        //
        // StartupApplicationExtraActionMessageAssist.Print();
        //
        // StartupMvcOptionExtraActionMessageAssist.Print();
        //
        // StartupEndPointExtraActionMessageAssist.Print();
        //
        // StartupConfigMessageAssist.Print();
        //
        // StartupDescriptionMessageAssist.Print();

        LogAssist.Hint(
            AuxiliaryConfigure.BuildHintMessage().ToArray()
        );

        LogAssist.Hint(
            SwaggerConfigure.BuildHintMessage().ToArray()
        );

        app.UseAutoMigrate();

        app.UseAutoEnsureCreated();

        StartupWarnMessageAssist.Print();

        app.RegisterWorks();

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
}