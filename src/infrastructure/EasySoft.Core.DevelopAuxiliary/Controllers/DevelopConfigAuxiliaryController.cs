using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.DevelopAuxiliary.Controllers;

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