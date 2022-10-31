using EasySoft.Core.Config.Utils;
using EasySoft.Core.Infrastructure.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class NLogInlayConfigController : BasicController
{
    public IActionResult Index()
    {
        var data = Tools.GetNlogDefaultConfig();

        return Content(data);
    }
}