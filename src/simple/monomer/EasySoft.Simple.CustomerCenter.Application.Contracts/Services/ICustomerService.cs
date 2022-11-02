using EasySoft.Core.Data.Attributes;
using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.CustomerCenter.Domain.Aggregates.CustomerAggregate;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.CustomerCenter.Application.Contracts.Services;

public interface ICustomerService : IBusinessService
{
    public Task<ExecutiveResult<Customer>> RegisterAsync(string loginName, string password);

    [UnitOfWork]
    public Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword);

    public Task<ExecutiveResult<Customer>> SignInAsync(string loginName, string password);
}