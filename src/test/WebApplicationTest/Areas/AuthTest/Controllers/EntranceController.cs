using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.AuthTest.Controllers;

public class EntranceController : AreaControllerCore
{
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignIn(string userName, string password)
    {
        var token = 123456.ToToken();

        this.SetCookie(GeneralConfigAssist.GetTokenName(), token);

        return Content("sign in success");
    }

    [Operator]
    public IActionResult Detail()
    {
        var token = this.GetToken();

        this.SetCookie(GeneralConfigAssist.GetTokenName(), token);

        return Content($"token:{token}");
    }

    [Operator]
    [GuidTag("65641b2706db4ddb8357082fa8860386")]
    public IActionResult AddData()
    {
        return Content("success");
    }
}