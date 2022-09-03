using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Areas.AuthTest.Controllers;

public class EntranceController : AreaControllerCore
{
    private readonly IAuthorService _authorService;

    public EntranceController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        var result = await _authorService.RegisterAsync("test", "123456");

        return this.WrapperExecutiveResult(result);
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignIn(string loginName, string password)
    {
        var result = _authorService.SignIn(loginName, password);

        if (!result.Success)
        {
            return Content(result.Message);
        }

        var token = result.Data?.AuthorId.ToToken();

        if (token != null)
        {
            this.SetCookie(GeneralConfigAssist.GetTokenName(), token);
        }

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