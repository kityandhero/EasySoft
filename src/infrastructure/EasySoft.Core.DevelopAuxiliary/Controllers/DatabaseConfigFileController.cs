namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class DatabaseConfigFileController : BasicController
{
    public IActionResult Index()
    {
        var data = new DatabaseConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = DatabaseConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }
}