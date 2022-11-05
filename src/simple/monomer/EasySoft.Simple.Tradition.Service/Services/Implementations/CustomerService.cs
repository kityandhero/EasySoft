using EasySoft.Core.Data.Repositories;
using EasySoft.Core.Data.Transactions;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(
        IUnitOfWork unitOfWork,
        IRepository<Customer> repository
    )
    {
        _unitOfWork = unitOfWork;
        _customerRepository = repository;
    }
}