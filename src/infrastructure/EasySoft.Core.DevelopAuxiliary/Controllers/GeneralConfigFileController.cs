namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class GeneralConfigFileController : BasicController
{
    public IActionResult Index()
    {
        var data = new GeneralConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = GeneralConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }
}