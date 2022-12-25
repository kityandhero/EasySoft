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