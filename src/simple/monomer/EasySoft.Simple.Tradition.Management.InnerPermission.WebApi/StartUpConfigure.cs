using EasySoft.Core.PermissionServer.Core.Assist;
using EasySoft.Core.PermissionVerification.Configures;

namespace EasySoft.Simple.Tradition.Management.InnerPermission.WebApi;

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
        Core.Assists.ApplicationAssist.InitManagement();
        PermissionConfigure.AddScanPermissionAssembly(typeof(Core.Assists.ApplicationAssist).Assembly);

        PermissionServerAssist.Init();

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