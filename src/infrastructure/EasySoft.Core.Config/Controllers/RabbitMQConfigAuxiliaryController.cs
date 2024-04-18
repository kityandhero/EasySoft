using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

/// <summary>
/// RabbitMQConfigAuxiliaryController
/// </summary>
public class RabbitMQConfigAuxiliaryController : BasicController
{
    /// <summary>
    /// GetTemplate
    /// </summary>
    /// <returns></returns>
    public IActionResult GetTemplate()
    {
        var data = new RabbitMQConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = RabbitMQConfigAssist.GetConfigFileInfo()
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
        return Content(await RabbitMQConfigAssist.GetConfigFileContent());
    }
}