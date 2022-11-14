using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.Core.Swagger.ConfigCollection;

namespace EasySoft.Core.Swagger.Controllers;

public class SwaggerConfigAuxiliaryController : BasicController
{
    public IActionResult GetTemplate()
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

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await SwaggerConfigAssist.GetConfigFileContent());
    }
}