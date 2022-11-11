using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.Core.Swagger.ConfigCollection;

namespace EasySoft.Core.Swagger.Controllers;

public class SwaggerConfigFileController : BasicController
{
    public IActionResult Index()
    {
        var data = new SwaggerConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = SwaggerConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }
}