using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

public class DevelopConfigAuxiliaryController : BasicController
{
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

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await DevelopConfigAssist.GetConfigFileContent());
    }
}