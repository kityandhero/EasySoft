using EasySoft.Simple.CustomerCenter.Application.Contracts.Services;
using EasySoft.Simple.CustomerCenter.Domain.Aggregates.CustomerAggregate;

namespace EasySoft.Simple.CustomerCenter.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(IRepository<Customer> repository)
    {
        _customerRepository = repository;
    }
}