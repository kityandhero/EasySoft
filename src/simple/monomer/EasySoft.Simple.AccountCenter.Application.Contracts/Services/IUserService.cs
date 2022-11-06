using EasySoft.Core.Data.Attributes;
using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.AccountCenter.Application.Contracts.DataTransferObjects.ApiParams;
using EasySoft.Simple.AccountCenter.Domain.Aggregates.AccountAggregate;
using EasySoft.Simple.DomainDrivenDesign.Application.Contracts.DataTransferObjects;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.AccountCenter.Application.Contracts.Services;

public interface IUserService : IBusinessService
{
    public Task<ExecutiveResult<User>> RegisterAsync(RegisterDto registerDto);

    [UnitOfWork]
    public Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword);

    public Task<ExecutiveResult<UserDto>> SignInAsync(SignInDto signInDto);
}