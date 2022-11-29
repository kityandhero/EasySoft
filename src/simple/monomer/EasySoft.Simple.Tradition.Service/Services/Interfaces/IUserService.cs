using EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;
using EasySoft.Simple.Tradition.Service.Events;

namespace EasySoft.Simple.Tradition.Service.Services.Interfaces;

/// <summary>
/// IUserService
/// </summary>
public interface IUserService : IBusinessService
{
    /// <summary>
    /// RegisterAsync
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    [UnitOfWork]
    public Task<ExecutiveResult<UserDto>> RegisterAsync(RegisterDto registerDto);

    /// <summary>
    /// 注册成功处理事件
    /// </summary>
    /// <param name="registerSuccessEvent"></param>
    /// <param name="tracker"></param>
    /// <returns></returns>
    public Task ProcessRegisterSuccessAsync(RegisterSuccessEvent registerSuccessEvent, IMessageTracker tracker)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// RegisterMultiAsync
    /// </summary>
    /// <param name="namePassword"></param>
    /// <returns></returns>
    [UnitOfWork]
    public Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword);

    /// <summary>
    /// SignInAsync
    /// </summary>
    /// <param name="signInDto"></param>
    /// <returns></returns>
    public Task<ExecutiveResult<UserDto>> SignInAsync(SignInDto signInDto);
}