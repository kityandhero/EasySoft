using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

public class RabbitMQConfigAuxiliaryController : BasicController
{
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

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await RabbitMQConfigAssist.GetConfigFileContent());
    }
}