namespace EasySoft.Simple.Tradition.Management.WebApi;

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
        ApplicationAssist.Init();

        // 配置额外的构建项目
        ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionServer")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
                })
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