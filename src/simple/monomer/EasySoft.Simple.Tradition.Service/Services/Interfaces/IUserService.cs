using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

namespace EasySoft.Simple.Tradition.Service.Services.Interfaces;

/// <summary>
/// IUserService
/// </summary>
public interface IUserService : IBusinessService
{
    /// <summary>
    /// RegisterAsync
    /// </summary>
    /// <param name="loginName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<ExecutiveResult<User>> RegisterAsync(string loginName, string password);

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