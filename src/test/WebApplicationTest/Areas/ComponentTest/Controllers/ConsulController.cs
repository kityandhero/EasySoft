using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Media.Image;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class ConsulController : AreaControllerCore
{
    public IActionResult Index()
    {
        var nlog = ConsulConfigAssist.GetValue("NLog");

        return Content(nlog);
    }
}