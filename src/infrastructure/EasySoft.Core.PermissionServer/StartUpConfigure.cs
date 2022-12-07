using EasySoft.Core.PermissionServer.Contexts;
using EasySoft.Core.PermissionServer.EntityConfigures;
using EasySoft.Core.PermissionServer.Operators;
using EasySoft.Core.PermissionServer.Services.Implementations;
using EasySoft.Core.PermissionServer.Services.Interfaces;

namespace EasySoft.Core.PermissionServer;

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
        AuxiliaryConfigure.PromptConfigFileInfo = true;

        EntityFramework.Configures.ContextConfigure.EnableDetailedErrors = true;
        EntityFramework.Configures.ContextConfigure.EnableSensitiveDataLogging = true;
        EntityFramework.Configures.ContextConfigure.AutoEnsureCreated = true;

        // 配置额外的构建项目
        ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceDbContext<DataContext>")
                .SetAction(applicationBuilder =>
                {
                    //使用 Sql Server
                    applicationBuilder.AddAdvanceSqlServer<PermissionSqlServerContext, IntegrationEntityConfigure>(
                        DatabaseConfigAssist.GetMainConnection(),
                        opt =>
                        {
                            //自动转换命名格式
                            opt.UseSnakeCaseNamingConvention();
                        }
                    );

                    // 使用 MySql
                    // applicationBuilder.AddAdvanceMySql<MySqlDataContext, IntegrationEntityConfigure>(
                    //     DatabaseConfigAssist.GetMainConnection(),
                    //     opt =>
                    //     {
                    //         //自动转换命名格式
                    //         opt.UseSnakeCaseNamingConvention();
                    //     }
                    // );

                    applicationBuilder.AddAssemblyBusinessServices(
                        typeof(ISecurityService).Assembly,
                        typeof(SecurityService).Assembly
                    );
                }),
            // 自定义静态文件配置 如有特殊需求，可以进行配置，不配置将采用内置选项，此处仅作为有需要时的样例
            // applicationBuilder => { applicationBuilder.AddStaticFileOptionsInjection<CustomStaticFileOptions>(); },
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceJsonWebToken")
                .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceJsonWebToken<ApplicationOperator>(); })
        );

        SwaggerConfigure.GeneralParameters.AddRange(
            new OpenApiParameter
            {
                Name = "token",
                Description = "登录凭据",
                Required = false,
                In = ParameterLocation.Header
            }
        );
    }
}