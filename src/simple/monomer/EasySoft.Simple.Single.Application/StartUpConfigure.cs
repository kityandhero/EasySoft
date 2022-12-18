using EasySoft.Simple.Single.Application.Common;
using EasySoft.Simple.Single.Application.PrepareStartWorks;
using EasySoft.Simple.Single.Application.Security;
using Timer = System.Timers.Timer;

namespace EasySoft.Simple.Single.Application;

/// <summary>
/// StartUpConfigure
/// </summary>
public class StartUpConfigure : IStartUpConfigure
{
    /// <summary>
    /// Init
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Init()
    {
        AuxiliaryConfigure.PromptStartupExecuteMessage = true;
        AuxiliaryConfigure.PromptConfigFileInfo = true;

        SwaggerConfigure.ExternalSchemaType.Add(typeof(Task<ApiResult>));

        SwaggerConfigure.GeneralParameters.AddRange(
            new OpenApiParameter
            {
                Name = "headerParam",
                Description = "全局 Header 参数",
                Required = true,
                In = ParameterLocation.Header
            },
            new OpenApiParameter
            {
                Name = "queryParam",
                Description = "全局 Query 参数",
                Required = false,
                In = ParameterLocation.Query
            }, new OpenApiParameter
            {
                Name = "cookieParam",
                Description = "全局 Cookie 参数",
                Required = false,
                In = ParameterLocation.Cookie
            }, new OpenApiParameter
            {
                Name = "pathParam",
                Description = "全局 Path 参数",
                Required = false,
                In = ParameterLocation.Path
            }
        );

        SwaggerConfigure.GeneralResponseHeaders.Add(
            new KeyValuePair<string, OpenApiHeader>(
                "responseHeader1",
                new OpenApiHeader
                {
                    Description = "common header"
                }
            )
        );

        SwaggerConfigure.SecurityScheme = new KeyValuePair<string, OpenApiSecurityScheme?>(
            GeneralConfigAssist.GetTokenName(),
            new OpenApiSecurityScheme
            {
                Description = "Cookie认证",
                Name = "Cookies",
                In = ParameterLocation.Cookie,
                Type = SecuritySchemeType.ApiKey
            }
        );

        Core.EntityFramework.Configures.ContextConfigure.EnableDetailedErrors = true;
        Core.EntityFramework.Configures.ContextConfigure.EnableSensitiveDataLogging = true;
        Core.EntityFramework.Configures.ContextConfigure.AutoEnsureCreated = true;

        // 配置额外的构建项目
        ApplicationConfigure.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddApiVersioning")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.Services.AddApiVersioning(o =>
                    {
                        //return versions in a response header
                        o.ReportApiVersions = true;
                        //default version select 
                        o.DefaultApiVersion = new ApiVersion(1, 0);
                        //if not specifying an api version,show the default version
                        o.AssumeDefaultVersionWhenUnspecified = true;
                    });
                }),
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPrepareStartWorkInjection")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.AddPrepareStartWorkInjection<SimplePrepareStartWork>();
                }),
            // 自定义静态文件配置 如有特殊需求，可以进行配置，不配置将采用内置选项，此处仅作为有需要时的样例
            // applicationBuilder => { applicationBuilder.AddStaticFileOptionsInjection<CustomStaticFileOptions>(); },
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceJsonWebToken")
                .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceJsonWebToken<ApplicationOperator>(); }),
            // applicationBuilder => { applicationBuilder.UseEasyToken<CustomTokenSecretOptions, ApplicationOperator>(); },
            // 自定义token密钥解析类
            // applicationBuilder => { applicationBuilder.UseEasyToken<CustomTokenSecretOptions, CustomTokenSecret, ApplicationOperator>(); },
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionVerification")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
                }),
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddExtraNormalInjection")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.AddExtraNormalInjection(containerBuilder =>
                    {
                        containerBuilder.RegisterType<SimpleDependencyInjection>().As<ISimpleDependencyInjection>()
                            .SingleInstance();
                    });
                }),
            // 启用日志面板
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceLogDashboard")
                .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceLogDashboard(); }),
            // 配置健康检测
            // applicationBuilder =>
            // {
            //     applicationBuilder.AddAdvanceHealthChecks(new List<IAdvanceHealthCheck>
            //     {
            //         new HelloHealthCheck().ToIAdvanceHealthCheck()
            //     });
            // },
            // SignalR
            new ExtraAction<WebApplicationBuilder>().SetName("AddSignalR").SetAction(applicationBuilder =>
            {
                applicationBuilder.Services.AddSignalR();
            })
        );

        ApplicationConfigure.AddAreas("AreaTest", "AuthTest", "DataTest", "ComponentTest");

        AgileConfigClientActionAssist.ActionAgileConfigChanged = _ =>
        {
            // LogAssist.Info("config changed");
        };

        ApplicationConfigure.AddTimer(
            5000,
            (services, _) =>
            {
                var logger = services.GetService<ILogger<object>>();

                logger?.LogAdvancePrompt("timer trigger.");
            }
        );
    }
}