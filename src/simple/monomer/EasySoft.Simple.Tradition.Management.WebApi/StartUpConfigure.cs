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
        Core.Assists.ApplicationAssist.InitManagement();

        var openApiParameter = new OpenApiParameter
        {
            Name = "token",
            Description = "登录凭据",
            Required = false,
            In = ParameterLocation.Header
        };

        SwaggerConfigure.GeneralParameters.AddRange(openApiParameter);
    }
}