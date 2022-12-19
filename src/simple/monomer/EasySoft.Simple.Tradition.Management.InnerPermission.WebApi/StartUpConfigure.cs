using EasySoft.Core.AppSecurityServer.Core.Entities;

namespace EasySoft.Simple.Tradition.Management.InnerPermission.WebApi;

/// <summary>
/// StartUpConfigure
/// </summary>
public class StartUpConfigure : IStartUpConfigure
{
    /// <summary>
    /// Init
    /// </summary>
    public void Init()
    {
        PermissionConfigure.AddRangeScanPermissionAssemblies(new List<Assembly>
        {
            typeof(Core.Assists.ApplicationAssist).Assembly,
            typeof(ErrorLog).Assembly,
            typeof(AppSecurity).Assembly
        });

        AppSecurityServerAssist.Init(true);

        PermissionServerAssist.Init(true);

        LogServerAssist.Init(true);

        Core.Assists.ApplicationAssist.InitManagement();

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