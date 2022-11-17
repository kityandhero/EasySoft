using EasySoft.Core.Web.Framework.Attributes;
using EasySoft.Core.Web.Framework.Filters;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceApplicationChannel = "9ef8c9f4-08b9-4755-912c-6ff80988f513";

    /// <summary>
    /// 标记当前应用通道值, 用于远程日志等数据中, 便于数据辨认, 不使用此方法标记, 框架将采用内置值 0 代替
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="channel"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static WebApplicationBuilder AddAdvanceApplicationChannel(
        this WebApplicationBuilder builder,
        int channel,
        string name
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceApplicationChannel))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceApplicationChannel)}."
        );

        builder.Host.AddAdvanceApplicationChannel(channel, name);

        return builder;
    }

    internal static WebApplicationBuilder AddAdvanceApplicationChannel<T>(
        this WebApplicationBuilder builder,
        T applicationChannel
    ) where T : IApplicationChannel
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceApplicationChannel))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceApplicationChannel)}<{typeof(T).Name}>."
        );

        builder.Host.AddAdvanceApplicationChannel(applicationChannel.GetChannel(), applicationChannel.GetName());

        return builder;
    }

    public static WebApplication EasyBuild(
        this WebApplicationBuilder builder
    )
    {
        WeaveApplicationBuilderExtraAction(builder);

        builder.AddAdvanceExceptionless();

        builder.Services.AddRouting(o => { o.LowercaseUrls = true; });

        builder.Services.AddApiVersioning(o =>
        {
            //return versions in a response header
            o.ReportApiVersions = true;
            //default version select 
            o.DefaultApiVersion = new ApiVersion(1, 0);
            //if not specifying an api version,show the default version
            o.AssumeDefaultVersionWhenUnspecified = true;
        });

        // AddMvc 最为全面， 涵盖 AddControllers 等的全部功能
        builder.Services.AddMvc(
                option =>
                {
                    option.EnableEndpointRouting = false;

                    if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.EasyToken &&
                        !FlagAssist.EasyTokenMiddlewareModeSwitch)
                        // 设置及接口数据返回格式
                        option.Filters.Add<EasyToken.Filters.OperatorFilter>();

                    if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.JsonWebToken &&
                        !FlagAssist.JsonWebTokenMiddlewareModeSwitch)
                        // 设置及接口数据返回格式
                        option.Filters.Add<JsonWebToken.Filters.OperatorFilter>();

                    if (FlagAssist.PermissionVerificationSwitch &&
                        !FlagAssist.PermissionVerificationMiddlewareModeSwitch)
                        // 设置及接口数据返回格式
                        option.Filters.Add<PermissionFilter>();

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
                options => { JsonConvertAssist.AdjustJsonSerializerSettings(options.SerializerSettings); }
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

        // 注入IHttpClientFactory 
        builder.Services.AddHttpClient();

        if (GeneralConfigAssist.GetCorsSwitch())
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    ConstCollection.DefaultSpecificOrigins,
                    configPolicy =>
                    {
                        configPolicy.WithOrigins(GeneralConfigAssist.GetCorsPolicies().ToArray());
                        configPolicy.AllowAnyHeader();
                        configPolicy.AllowAnyMethod();
                        configPolicy.AllowCredentials();
                    }
                );
            });

        builder.Services.AddEndpointsApiExplorer();

        builder.AddAdvanceSwagger();

        builder.AddAdvanceEasyCaching();

        builder.AddAdvanceMiniProfile();

        builder.AddGeneralLogTransmitter();

        builder.AddErrorLogTransmitter();

        builder.AddAdvanceCap();

        builder.AddAdvanceHangfire();

        var app = builder.Build();

        LogAssist.SetLogger(app.Logger);

        AutofacAssist.Instance.SetContainer(app.UseHostFiltering().ApplicationServices.GetAutofacRoot());

        ServiceAssist.SetServiceProvider(app.Services);

        FlagAssist.SetApplicationRunPerformed();

        // 中间件调用顺序请参阅: https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order

        // ASP.NET Core MVC的过滤器类别以及默认执行顺序：
        // https://www.cnblogs.com/yangxu-pro/p/9010978.html
        // 授权过滤器，它是第一个运行的，它的作用就是判断HTTP Context中的用户是否拥有当前请求的权限，如果用户没有权限，那么它就会“短路”管道。
        // 资源过滤器，在授权过滤器后运行，在管道其它动作之前，和管道动作都结束后运行。它可以实现缓存或由于性能原因执行短路操作。它在实体绑定之前运行，所以它也可以对影响实体绑定。
        // Action过滤器，它在Action方法调用之前和之后立即执行，它可以操作传进Action的参数和返回的结果。
        // 异常过滤器，针对在写入响应Body之前发生的未处理的异常，它可以应用全局的策略，
        // 结果过滤器，它可以在每个Action结果执行之前和之后运行代码，但也只是在Action方法无错误的成功完成后才可以执行。

        if (GeneralConfigAssist.GetForwardedHeadersSwitch())
        {
            // 配置反向代理服务器, 需要在调用其他中间件之前 
            // https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-6.0
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            StartupConfigMessageAssist.AddConfig(
                "ForwardedHeadersSwitch: enable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }
        else
        {
            StartupConfigMessageAssist.AddConfig(
                "ForwardedHeadersSwitch: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }

        app.UsePrepareStartWork();

        if (!app.Environment.IsDevelopment())
            // app.UseExceptionHandler("/Error");
            app.UseHsts();

        if (GeneralConfigAssist.GetHttpRedirectionHttpsSwitch())
        {
            // 当前项目启动后，监听的是否是多个端口，其中如果有协议是Https—我们在访问Http的默认会转发到Https中
            app.UseHttpsRedirection();

            StartupConfigMessageAssist.AddConfig(
                "HttpRedirectionHttpsSwitch: enable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }
        else
        {
            StartupConfigMessageAssist.AddConfig(
                "HttpRedirectionHttpsSwitch: disabled.",
                GeneralConfigAssist.GetConfigFileInfo()
            );
        }

        if (GeneralConfigAssist.GetUseStaticFilesSwitch())
        {
            app.UseAdvanceStaticFiles();

            var staticFileOptionsTypeName = "";

            if (FlagAssist.GetAdvanceStaticFileOptionsSwitch())
                staticFileOptionsTypeName = AutofacAssist.Instance.Resolve<AdvanceStaticFileOptions>().GetType().Name;

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

        app.UseRouting();

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
            app.UseEasyTokenMiddleware();

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.JsonWebToken &&
            FlagAssist.JsonWebTokenMiddlewareModeSwitch)
            app.UseJsonWebTokenMiddleware();

        if (!string.IsNullOrWhiteSpace(FlagAssist.TokenMode))
        {
            if (FlagAssist.EasyTokenMiddlewareModeSwitch || FlagAssist.JsonWebTokenMiddlewareModeSwitch)
                StartupConfigMessageAssist.AddConfig(
                    $"TokenMode: {FlagAssist.TokenMode}, use middleware mode, TokenServerDumpSwitch: {GeneralConfigAssist.GetTokenServerDumpSwitch()}, TokenParseFromUrlSwitch: {GeneralConfigAssist.GetTokenParseFromUrlSwitch()}, TokenParseFromCookieSwitch: {GeneralConfigAssist.GetTokenParseFromCookieSwitch()}.",
                    GeneralConfigAssist.GetConfigFileInfo()
                );
            else
                StartupConfigMessageAssist.AddConfig(
                    $"TokenMode: {FlagAssist.TokenMode}, use filter mode, TokenServerDumpSwitch: {GeneralConfigAssist.GetTokenServerDumpSwitch()}, TokenParseFromUrlSwitch: {GeneralConfigAssist.GetTokenParseFromUrlSwitch()}, TokenParseFromCookieSwitch: {GeneralConfigAssist.GetTokenParseFromCookieSwitch()}.",
                    GeneralConfigAssist.GetConfigFileInfo()
                );
        }

        if (FlagAssist.PermissionVerificationSwitch)
        {
            if (string.IsNullOrWhiteSpace(FlagAssist.TokenMode))
                throw new Exception("use PermissionVerification need config one of token mode");

            if (FlagAssist.PermissionVerificationMiddlewareModeSwitch) app.UsePermissionVerificationMiddleware();

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

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteErrorLogSwitch()
                ? "RemoteErrorLogEnable: enable."
                : "RemoteErrorLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteGeneralLogSwitch()
                ? "RemoteGeneralLogEnable: enable."
                : "RemoteGeneralLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseAdvanceEnvironmentAuxiliary();

        WeaveApplicationExtraAction(app);

        app.UseAdvanceMapControllers();

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

        // StartupConfigMessageAssist.Add(
        //     new StartupMessage()
        //         .SetLevel(LogLevel.Information)
        //         .SetMessage(
        //             UtilityTools.Standard.ConstCollection.ApplicationStartMessageDivider
        //         )
        // );
        //
        // StartupDescriptionMessageAssist.Add(
        //     new StartupMessage()
        //         .SetLevel(LogLevel.Information)
        //         .SetMessage(
        //             UtilityTools.Standard.ConstCollection.ApplicationStartMessageDivider
        //         )
        // );

        StartupBuilderExtraActionMessageAssist.Print();

        StartupApplicationExtraActionMessageAssist.Print();

        StartupMvcOptionExtraActionMessageAssist.Print();

        StartupEndPointExtraActionMessageAssist.Print();

        StartupConfigMessageAssist.Print();

        StartupDescriptionMessageAssist.Print();

        LogAssist.Hint(
            AuxiliaryConfigure.BuildHintMessage().ToArray()
        );

        LogAssist.Hint(
            SwaggerConfigure.BuildHintMessage().ToArray()
        );

        app.UseAutoMigrate();

        app.UseAutoEnsureCreated();

        StartupWarnMessageAssist.Print();

        return app;
    }

    private static string GetWebRootPath()
    {
        var result = EnvironmentAssist.GetEnvironment().WebRootPath;

        if (!FlagAssist.GetAdvanceStaticFileOptionsSwitch()) return result;

        if (!FlagAssist.GetAdvanceStaticFileOptionsSwitch()) return result;

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

        if (extraActions.Count <= 0) return;

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

            if (action == null) return;

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

        if (extraActions.Count <= 0) return;

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

            if (action == null) return;

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

        if (extraActions.Count <= 0) return;

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

            if (action == null) return;

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