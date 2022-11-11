namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class ElasticSearchConfigFileController : BasicController
{
    public IActionResult Index()
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
}