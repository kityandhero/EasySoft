using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

/// <summary>
/// DevelopConfigAuxiliaryController
/// </summary>
public class DevelopConfigAuxiliaryController : BasicController
{
    /// <summary>
    /// GetTemplate
    /// </summary>
    /// <returns></returns>
    public IActionResult GetTemplate()
    {
        var data = new DevelopConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = DevelopConfigAssist.GetConfigFileInfo()
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
        return Content(await DevelopConfigAssist.GetConfigFileContent());
    }
}