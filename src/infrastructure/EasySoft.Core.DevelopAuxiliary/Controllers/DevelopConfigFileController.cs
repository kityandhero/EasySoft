using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Infrastructure.Controllers;
using EasySoft.UtilityTools.Core.ExtensionMethods;

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