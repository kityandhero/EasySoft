using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

/// <summary>
/// ElasticSearchConfigAuxiliaryController
/// </summary>
public class ElasticSearchConfigAuxiliaryController : BasicController
{
    /// <summary>
    /// GetTemplate
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// GetCurrent
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetCurrent()
    {
        return Content(await ElasticSearchConfigAssist.GetConfigFileContent());
    }
}