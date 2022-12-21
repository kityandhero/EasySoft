using EasySoft.Core.Gateway.Ocelot.ConfigAssist;
using EasySoft.Core.Gateway.Ocelot.ConfigCollection;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Core.Gateway.Ocelot.Controllers;

/// <summary>
/// OcelotConfigAuxiliaryController
/// </summary>
public class OcelotConfigAuxiliaryController : BasicController
{
    /// <summary>
    /// GetTemplate
    /// </summary>
    /// <returns></returns>
    public IApiResult GetTemplate()
    {
        var data = new OcelotConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = OcelotConfigAssist.GetConfigFileInfo()
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
        return Content(await OcelotConfigAssist.GetConfigFileContent());
    }
}