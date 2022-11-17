using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

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