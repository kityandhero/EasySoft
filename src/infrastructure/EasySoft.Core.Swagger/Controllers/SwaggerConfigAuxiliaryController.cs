using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.Core.Swagger.ConfigCollection;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Core.Swagger.Controllers;

/// <summary>
/// SwaggerConfigAuxiliaryController
/// </summary>
public class SwaggerConfigAuxiliaryController : BasicController
{
    /// <summary>
    /// GetTemplate
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// GetCurrent
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetCurrent()
    {
        return Content(await SwaggerConfigAssist.GetConfigFileContent());
    }
}