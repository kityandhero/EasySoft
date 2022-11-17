using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

public class ElasticSearchConfigAuxiliaryController : BasicController
{
    public IActionResult GetTemplate()
    {
        var data = new ElasticSearchConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = ElasticSearchConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await ElasticSearchConfigAssist.GetConfigFileContent());
    }
}