namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class RabbitMQConfigFileController : BasicController
{
    public IActionResult Index()
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
}