using EasySoft.Core.AuthenticationCore.Assists;
using EasySoft.Core.EasyToken.ExtensionMethods;
using EasySoft.Core.EnvironmentAuxiliary.ExtensionMethods;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Infrastructure.Extensions;
using EasySoft.Core.Infrastructure.Options;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.JsonWebToken.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Assists;
using EasySoft.Core.PermissionVerification.Extensions;
using EasySoft.Core.PermissionVerification.Filters;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using EasySoft.Core.Web.Framework.Kernel.ActionModelConventions;
using EasySoft.Core.Web.Framework.Kernel.Assists;
using EasySoft.Core.Web.Framework.Kernel.Attributes;
using EasySoft.Core.Web.Framework.Kernel.Filters;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Web.Framework.Kernel.ExtensionMethods.WebApplicationBuilderExtensions;

public static class BuildExtensions
{
    public static WebApplication BuildWebApplication(
        this WebApplicationBuilder builder
    )
    {
        // AddMvc 最为全面， 涵盖 AddControllers 等的全部功能
        builder.Services.AddMvc(option =>
                {
                    option.EnableEndpointRouting = false;

                    option.Conventions.Add(new IgnoreForApiExplorerActionModelConvention());

                    if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.EasyToken &&
                        !FlagAssist.EasyTokenMiddlewareModeSwitch)

                        // 设置及接口数据返回格式
                    {
                        option.Filters.Add<EasyToken.Filters.OperatorFilter>();
                    }

                    if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.JsonWebToken &&
                        !FlagAssist.JsonWebTokenMiddlewareModeSwitch)

                        // 设置及接口数据返回格式
                    {
                        option.Filters.Add<JsonWebToken.Filters.OperatorFilter>();
                    }

                    if (FlagAssist.PermissionVerificationSwitch &&
                        !FlagAssist.PermissionVerificationMiddlewareModeSwitch)

                        // 设置及接口数据返回格式
                    {
                        option.Filters.Add<PermissionFilter>();
                    }

                    // 设置及接口数据返回格式
                    option.Filters.Add<ApiResultFilterAttribute>();

                    // 设置全局异常过滤器
                    option.Filters.Add<GlobalExceptionFilter>();

                    option.WeaveMvcOptionExtraAction();
                }
            )

            // 爆露ApplicationPartManager 实例给外部工具，用以实现某些特定功能
            .ConfigureApplicationPartManager(ApplicationPartManagerAssist.SetApplicationPartManager)
            .AddControllersAsServices()
            .AddNewtonsoftJson(options =>
                {
                    JsonConvertAssist.AdjustJsonSerializerSettings(options.SerializerSettings);
                }
            );

        builder.WeaveApplicationBuilderExtraAction();

        var app = builder.Build();

        #region SetProvider

        StartupDescriptionMessageAssist.AddPrompt(
            $"SetProvider after build application."
        );

        ServiceProviderAssist.SetProvider(app.Services);

        #endregion

        FlagAssist.SetApplicationRunPerformed();

        LogAssist.SetLogger(app.Logger);

        #region ForwardedHeaders

        var forwardedHeadersConfigValue = KernelConfigureAssist.GetForwardedHeaders(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            forwardedHeadersConfigValue.ToArrowDescription()
        );

        if (forwardedHeadersConfigValue.Value)
        {
            // 配置反向代理服务器, 需要在调用其他中间件之前 
            // https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-6.0
            // https: //learn.microsoft.com/zh-cn/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-6.0
            app.UseForwardedHeaders(
                new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                }
            );
        }

        #endregion

        #region Hsts

        var hstsSwitchConfigValue = KernelConfigureAssist.GetHstsSwitch(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            hstsSwitchConfigValue.ToArrowDescription()
        );

        if (!app.Environment.IsDevelopment())
        {
            if (hstsSwitchConfigValue.Value)
            {
                app.UseHsts();
            }
        }

        #endregion

        #region HttpRedirectionHttps

        var httpRedirectionHttpsConfigValue = KernelConfigureAssist.GetHttpRedirectionHttps(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            httpRedirectionHttpsConfigValue.ToArrowDescription()
        );

        if (httpRedirectionHttpsConfigValue.Value)
        {
            // 当前项目启动后，监听的是否是多个端口，其中如果有协议是Https—我们在访问Http的默认会转发到Https中
            app.UseHttpsRedirection();
        }

        #endregion

        #region UseStaticFiles

        var useStaticFilesConfigValue = InfrastructureConfigureAssist.GetUseStaticFiles(builder.Configuration);

        if (useStaticFilesConfigValue.Value)
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
                $"{useStaticFilesConfigValue.Node} -> enable, mode: {(FlagAssist.GetAdvanceStaticFileOptionsSwitch() ? $"custom, config class is \"{staticFileOptionsTypeName}\"" : "default")}."
            );
        }
        else
        {
            StartupConfigMessageAssist.AddConfig(
                useStaticFilesConfigValue.ToArrowDescription()
            );
        }

        #endregion

        app.UsePrepareStartWork();

        app.UseRouting();

        var webRootPathConfigValue = InfrastructureConfigureAssist.GetWebRootPath(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            webRootPathConfigValue.ToArrowDescription()
        );

        #region Authentication

        app.UseAuthentication();

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

        var accessWayDetectSwitchConfigValue = AuthorizationConfigureAssist.GetAccessWayDetectSwitch(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            accessWayDetectSwitchConfigValue.ToArrowDescription()
        );

        if (!string.IsNullOrWhiteSpace(FlagAssist.TokenMode))
        {
            var tokenServerDumpSwitchConfigValue = AuthenticationConfigureAssist.GetTokenServerDumpSwitch(
                builder.Configuration
            );

            StartupConfigMessageAssist.AddConfig(
                tokenServerDumpSwitchConfigValue.ToArrowDescription()
            );

            var tokenParseFromUrlSwitchConfigValue = AuthenticationConfigureAssist.GetTokenParseFromUrlSwitch(
                builder.Configuration
            );

            StartupConfigMessageAssist.AddConfig(
                tokenParseFromUrlSwitchConfigValue.ToArrowDescription()
            );

            var tokenParseFromCookieSwitchConfigValue = AuthenticationConfigureAssist.GetTokenParseFromCookieSwitch(
                builder.Configuration
            );

            StartupConfigMessageAssist.AddConfig(
                tokenParseFromCookieSwitchConfigValue.ToArrowDescription()
            );

            if (FlagAssist.EasyTokenMiddlewareModeSwitch || FlagAssist.JsonWebTokenMiddlewareModeSwitch)
            {
                StartupConfigMessageAssist.AddConfig(
                    "Token verification mode use middleware mode."
                );
            }
            else
            {
                StartupConfigMessageAssist.AddConfig(
                    $"Token verification mode use filter mode."
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

        #endregion

        app.WeaveApplicationExtraAction();

        app.UseAdvanceMapControllers();

        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAdvanceEnvironmentAuxiliary();

        // StartupConfigMessageAssist.AddConfig(
        //     $"Environment is {EnvironmentAssist.GetEnvironment().EnvironmentName}."
        // );

        // StartupConfigMessageAssist.AddConfig(
        //     $"EnvironmentAlias is {EnvironmentAssist.GetEnvironmentAliasName()}."
        // );

        // StartupConfigMessageAssist.AddConfig(
        //     $"ContentRootPath is \"{EnvironmentAssist.GetEnvironment().ContentRootPath}\"."
        // );
        //
        // StartupDescriptionMessageAssist.AddPrompt(
        //     $"Application start completed{(!FlagAssist.StartupUrls.Any() ? "." : $" at {FlagAssist.StartupUrls.Join(" ")}.")}"
        // );

        #region Startup Info Print

        StartupBuilderExtraActionMessageAssist.Print();

        StartupApplicationExtraActionMessageAssist.Print();

        StartupMvcOptionExtraActionMessageAssist.Print();

        StartupEndPointExtraActionMessageAssist.Print();

        StartupConfigMessageAssist.Print();

        StartupDescriptionMessageAssist.Print();

        #endregion

        // ------------------------

        LogAssist.Hint(
            AuxiliaryConfigure.BuildHintMessage().ToArray()
        );

        #region Startup Warn Print

        StartupWarnMessageAssist.Print();

        #endregion

        app.RegisterWorks();

        return app;
    }

    /// <summary>
    /// 织入扩展的 Application Action
    /// </summary>
    private static void WeaveApplicationBuilderExtraAction(
        this WebApplicationBuilder builder
    )
    {
        var extraActions = ApplicationConfigure.GetAllWebApplicationBuilderExtraActions().ToList();

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
            }
        );
    }
}