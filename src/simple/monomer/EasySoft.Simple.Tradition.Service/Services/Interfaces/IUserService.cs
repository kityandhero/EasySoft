using EasySoft.Core.Data.Attributes;
using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Tradition.Service.Services.Interfaces;

public interface IUserService : IBusinessService
{
    public Task<ExecutiveResult<User>> RegisterAsync(string loginName, string password);

    [UnitOfWork]
    public Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword);

    public Task<ExecutiveResult<UserDto>> SignInAsync(SignInDto signInDto);
}