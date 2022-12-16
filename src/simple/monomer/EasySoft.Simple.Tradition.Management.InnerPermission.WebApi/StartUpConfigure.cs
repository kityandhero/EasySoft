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

        PermissionConfigure.AddRangeScanPermissionAssemblies(new List<Assembly>
        {
            typeof(Core.Assists.ApplicationAssist).Assembly,
            typeof(ErrorLog).Assembly
        });

        PermissionServerAssist.Init();

        LogServerAssist.Init();

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