using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.Core.Swagger.ConfigCollection;
using EasySoft.UtilityTools.Core.Results;

namespace EasySoft.Core.Swagger.Controllers;

public class SwaggerConfigAuxiliaryController : BasicController
{
    public IApiResult GetTemplate()
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