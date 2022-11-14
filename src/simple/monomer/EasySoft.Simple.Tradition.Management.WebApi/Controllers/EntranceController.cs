﻿using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.Simple.Tradition.Management.WebApi.Common;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Tradition.Management.WebApi.Controllers;

/// <summary>
/// EntranceController
/// </summary>
public class EntranceController : ControllerCore
{
    private readonly IUserService _userService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="userService"></param>
    public EntranceController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Register()
    {
        var result = await _userService.RegisterAsync("test", "123456");

        return this.WrapperExecutiveResult(result);
    }

    // /// <summary>
    // /// SignIn
    // /// </summary>
    // /// <returns></returns>
    // [HttpGet]
    // public IActionResult SignIn()
    // {
    //     return View();
    // }

    /// <summary>
    /// SignIn
    /// </summary>
    /// <param name="loginName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SignIn(string loginName, string password)
    {
        var result = await _userService.SignInAsync(loginName, password);

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