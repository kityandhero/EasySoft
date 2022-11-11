namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class DevelopConfigFileController : BasicController
{
    public IActionResult Index()
    {
        var data = new DevelopConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = DevelopConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }
}