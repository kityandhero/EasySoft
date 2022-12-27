using EasySoft.Simple.Tradition.Management.Core.Common;

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
        if (signInDto.AccountName == UserConstCollection.SuperAdministrator) return await SuperSignIn(signInDto);

        var result = await _userService.SignInAsync(signInDto);

        return this.WrapperExecutiveResult(
            result,
            o => new
            {
                token = o.UserId.ToToken(),
                authorities = new List<string>()
            }
        );
    }

    /// <summary>
    /// 超级登录
    /// </summary>
    /// <param name="signInDto"></param>
    /// <returns></returns>
    [Route("superSignIn")]
    [HttpPost]
    protected Task<IApiResult> SuperSignIn(SignInDto signInDto)
    {
        var result = SuperAdministratorAssist.SuperSignIn(signInDto, out var authorities);

        return Task.FromResult(this.WrapperExecutiveResult(
            result,
            o => new
            {
                token = UserConstCollection.SuperAdministratorId.ToToken(),
                authorities
            }
        ));
    }
}