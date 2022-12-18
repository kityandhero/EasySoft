using EasySoft.Simple.AccountCenter.Application.Contracts.DataTransferObjects.ApiParams;
using EasySoft.Simple.AccountCenter.Application.Contracts.ExtensionMethods;
using EasySoft.Simple.AccountCenter.Application.Contracts.Services;
using EasySoft.Simple.DomainDrivenDesign.Infrastructure.Controllers;
using EasySoft.UtilityTools.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.AccountCenter.WebApi.Controllers;

/// <summary>
/// EntranceController
/// </summary>
[Route("entrance")]
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
    /// 账户注册
    /// </summary>
    /// <returns></returns>
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _userService.RegisterAsync(registerDto);

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data?.ToUserDto());
    }

    /// <summary>
    /// 账户登录
    /// </summary>
    /// <returns></returns>
    [Route("signIn")]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
    {
        var result = await _userService.SignInAsync(signInDto);

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }
}