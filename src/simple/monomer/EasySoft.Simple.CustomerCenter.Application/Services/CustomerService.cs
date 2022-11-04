using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.CustomerCenter.Application.Contracts.Services;
using EasySoft.Simple.CustomerCenter.Domain.Aggregates.CustomerAggregate;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.CustomerCenter.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(IRepository<Customer> repository)
    {
        _customerRepository = repository;
    }
}