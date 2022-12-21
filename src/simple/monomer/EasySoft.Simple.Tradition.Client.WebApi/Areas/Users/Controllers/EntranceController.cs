using EasySoft.Simple.Tradition.Client.WebApi.Areas.Users.Controllers.Bases;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Simple.Tradition.Client.WebApi.Areas.Users.Controllers;

/// <summary>
/// 应用入口
/// </summary>
[Route("entrance")]
public class EntranceController : AreaControllerCore
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
    [Route("signIn")]
    [HttpPost]
    public async Task<IApiResult> SignIn(SignInDto signInDto)
    {
        var result = await _userService.SignInAsync(signInDto);

        return this.WrapperExecutiveResult(
            result,
            o => new
            {
                token = o.UserId.ToToken()
            }
        );
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    [Route("register")]
    [HttpPost]
    public async Task<IApiResult> Register(RegisterDto registerDto)
    {
        var result = await _userService.RegisterAsync(registerDto);

        return this.WrapperExecutiveResult(
            result,
            o => new
            {
                token = o.UserId.ToToken()
            }
        );
    }
}