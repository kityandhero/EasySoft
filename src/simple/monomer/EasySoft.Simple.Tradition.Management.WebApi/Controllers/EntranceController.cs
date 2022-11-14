using EasySoft.Simple.Tradition.Management.WebApi.Common;
using EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Management.WebApi.Controllers;

/// <summary>
/// 应用入口
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
    /// 登录
    /// </summary>
    /// <param name="signInDto"></param>
    /// <returns></returns>
    [HttpPost("signIn", Name = "SignIn")]
    public async Task<IActionResult> SignIn(SignInDto signInDto)
    {
        var result = await _userService.SignInAsync(signInDto);

        return this.WrapperExecutiveResult(
            result,
            o => new
            {
                token = o.UserId.ToToken()
            });
    }
}