using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class MongoConfigAuxiliaryController : BasicController
{
    public IActionResult GetTemplate()
    {
        var data = new MongoConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = MongoConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await MongoConfigAssist.GetConfigFileContent());
    }
}