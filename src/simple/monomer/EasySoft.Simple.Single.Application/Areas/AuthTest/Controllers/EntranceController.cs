using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Single.Application.Areas.AuthTest.Controllers;

/// <summary>
/// EntranceController
/// </summary>
public class EntranceController : AreaControllerCore
{
    private readonly IAuthorService _authorService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="authorService"></param>
    public EntranceController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Register()
    {
        var result = await _authorService.RegisterAsync("test", "123456");

        return this.WrapperExecutiveResult(result);
    }

    /// <summary>
    /// SignIn
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    /// <summary>
    /// SignIn
    /// </summary>
    /// <param name="loginName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SignIn(string loginName, string password)
    {
        var result = await _authorService.SignInAsync(loginName, password);

        if (!result.Success) return Content(result.Message);

        var token = result.Data?.Id.ToToken();

        if (token != null) this.SetCookie(GeneralConfigAssist.GetTokenName(), token);

        return Content("sign in success");
    }

    /// <summary>
    /// Detail
    /// </summary>
    /// <returns></returns>
    [Operator]
    public IActionResult Detail()
    {
        var token = this.GetToken();

        this.SetCookie(GeneralConfigAssist.GetTokenName(), token);

        return Content($"token:{token}");
    }

    /// <summary>
    /// AddData
    /// </summary>
    /// <returns></returns>
    [Operator]
    [GuidTag("65641b2706db4ddb8357082fa8860386")]
    public IActionResult AddData()
    {
        return Content("success");
    }
}