using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Service.Services.Implements;

/// <summary>
/// CustomerService
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRepository<Customer> _customerRepository;

    /// <summary>
    /// CustomerService
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="repository"></param>
    public CustomerService(
        IUnitOfWork unitOfWork,
        IRepository<Customer> repository
    )
    {
        _unitOfWork = unitOfWork;
        _customerRepository = repository;
    }
}