namespace EasySoft.Simple.Tradition.Client.WebApi.Areas.AuthTest.Controllers;

/// <summary>
/// EntranceController
/// </summary>
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
    /// Register
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Register()
    {
        var result = await _userService.RegisterAsync(new RegisterDto()
        {
            LoginName = "test",
            Password = "123456"
        });

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
    /// <param name="signInDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
    {
        var result = await _userService.SignInAsync(signInDto);

        if (!result.Success) return Content(result.Message);

        var token = result.Data?.UserId.ToToken();

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