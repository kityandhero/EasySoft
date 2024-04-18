using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

/// <summary>
/// MongoConfigAuxiliaryController
/// </summary>
public class MongoConfigAuxiliaryController : BasicController
{
    /// <summary>
    /// GetTemplate
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// GetCurrent
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetCurrent()
    {
        return Content(await MongoConfigAssist.GetConfigFileContent());
    }
}