using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.NLog.Controllers;

public class NLogInlayConfigController : BasicController
{
    public IActionResult Index()
    {
        var data = JsonConvertAssist.FormatText(Tools.GetNlogEmbedConfig());

        return Content(data);
    }
}