using EasySoft.Simple.Tradition.Management.Core.Common;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Simple.Tradition.Management.Core.Controllers;

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
    [Route("signIn")]
    [HttpPost]
    // [ProducesResponseType(typeof(IApiResult), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
}