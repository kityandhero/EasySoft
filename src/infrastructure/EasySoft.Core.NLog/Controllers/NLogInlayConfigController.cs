using EasySoft.Core.Config.Utils;
using EasySoft.Core.Infrastructure.Controllers;

namespace EasySoft.Core.NLog.Controllers;

public class NLogInlayConfigController : BasicController
{
    public IActionResult Index()
    {
        var data = Tools.GetNlogDefaultConfig();

        return Content(data);
    }
}