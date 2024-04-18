using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

/// <summary>
/// GeneralConfigAuxiliaryController
/// </summary>
public class GeneralConfigAuxiliaryController : BasicController
{
    /// <summary>
    /// GetTemplate
    /// </summary>
    /// <returns></returns>
    public IActionResult GetTemplate()
    {
        var data = new GeneralConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = GeneralConfigAssist.GetConfigFileInfo()
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
        return Content(await GeneralConfigAssist.GetConfigFileContent());
    }
}