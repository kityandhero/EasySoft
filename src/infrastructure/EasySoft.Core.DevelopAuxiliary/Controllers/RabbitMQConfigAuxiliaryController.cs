using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.DevelopAuxiliary.Controllers;

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