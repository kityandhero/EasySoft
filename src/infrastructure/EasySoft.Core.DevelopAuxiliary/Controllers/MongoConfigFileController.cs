namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class MongoConfigFileController : BasicController
{
    public IActionResult Index()
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
}