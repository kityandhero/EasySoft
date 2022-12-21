using EasySoft.Core.PermissionServer.Contexts;
using EasySoft.Core.PermissionServer.Core.Assist;
using EasySoft.Core.PermissionServer.Core.Subscribers;
using EasySoft.Core.PermissionServer.Operators;

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

        ContextConfigure.EnableDetailedErrors = true;
        ContextConfigure.EnableSensitiveDataLogging = true;
        ContextConfigure.AutoEnsureCreated = true;

        PermissionServerAssist.Init(false);

        // 配置额外的构建项目
        ApplicationConfigure.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceDbContext<DataContext>")
                .SetAction(applicationBuilder =>
                {
                    //使用 Sql Server
                    applicationBuilder.AddAdvanceSqlServer<PermissionSqlServerContext>(
                        DatabaseConfigAssist.GetMainConnection()
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