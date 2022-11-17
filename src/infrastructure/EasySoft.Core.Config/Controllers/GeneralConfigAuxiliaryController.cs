using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

public class GeneralConfigAuxiliaryController : BasicController
{
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

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await GeneralConfigAssist.GetConfigFileContent());
    }
}