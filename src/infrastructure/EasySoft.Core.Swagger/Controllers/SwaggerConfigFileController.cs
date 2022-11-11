using EasySoft.Core.Infrastructure.Controllers;
using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.Core.Swagger.ConfigCollection;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

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