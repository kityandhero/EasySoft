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
            });
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
            });
    }

    /// <summary>
    /// Test1
    /// </summary>
    /// <returns></returns>
    [Route("test1")]
    [HttpPost]
    public IApiResult<UserDto> Test1()
    {
        return new ApiResult<UserDto>(ReturnCode.Ok)
        {
            Data = new UserDto()
        };
    }

    /// <summary>
    /// Test2
    /// </summary>
    /// <returns></returns>
    [Route("test2")]
    [HttpPost]
    public ApiResult<UserDto, UserDto> Test2()
    {
        return new ApiResult<UserDto, UserDto>(ReturnCode.Ok)
        {
            Data = new UserDto(),
            ExtraData = new UserDto()
        };
    }
}