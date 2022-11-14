using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.DevelopAuxiliary.Controllers;

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