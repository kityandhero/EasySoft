using EasySoft.Simple.Tradition.Management.WebApi.Common;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

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
}