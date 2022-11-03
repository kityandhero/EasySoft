using EasySoft.Core.Data.Repositories;
using EasySoft.Core.Data.Transactions;
using EasySoft.Core.EntityFramework.EntityFactories;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRepository<Customer> _customerRepository;

    private readonly IRepository<Blog> _blogRepository;

    public CustomerService(
        IUnitOfWork unitOfWork,
        IRepository<Customer> repository,
        IRepository<Blog> blogRepository
    )
    {
        _unitOfWork = unitOfWork;
        _customerRepository = repository;
        _blogRepository = blogRepository;
    }

    public async Task<ExecutiveResult<Customer>> RegisterAsync(string loginName, string password)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            return new ExecutiveResult<Customer>(ReturnCode.ParamError.ToMessage("登陆名不能为空白"));

        if (string.IsNullOrWhiteSpace(password))
            return new ExecutiveResult<Customer>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        var result = await _customerRepository.ExistAsync(o => o.LoginName == loginName);

        if (result.Success)
            return new ExecutiveResult<Customer>(ReturnCode.NoChange.ToMessage("登录名已存在"));

        try
        {
            var customer = EntityFactory.Create<Customer>();
            var blog = EntityFactory.Create<Blog>();

            customer.LoginName = loginName;
            customer.Password = password.ToMd5();

            blog.CustomerId = customer.Id;

            var resultAddCustomer = await _customerRepository.AddAsync(customer);

            if (!resultAddCustomer.Success)
                throw new Exception(resultAddCustomer.Message);

            var resultAddBlog = await _blogRepository.AddAsync(blog);

            if (!resultAddBlog.Success)
                throw new Exception(resultAddBlog.Message);

            await _unitOfWork.CommitAsync();

            return resultAddCustomer;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return new ExecutiveResult<Customer>(ReturnMessage.Exception.ToMessage(ex.Message));
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    public async Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword)
    {
        if (namePassword.Count <= 0) return new ExecutiveResult(ReturnCode.NoChange.ToMessage("无可添加数据"));

        try
        {
            var customers = new List<Customer>();
            var blogs = new List<Blog>();

            foreach (var item in namePassword)
            {
                var customer = EntityFactory.Create<Customer>();
                var blog = EntityFactory.Create<Blog>();

                customer.LoginName = item.Key;
                customer.Password = item.Value;

                blog.CustomerId = customer.Id;

                customers.Add(customer);
                blogs.Add(blog);
            }

            var result = await _customerRepository.AddRangeAsync(customers);

            if (!result.Success)
                throw new Exception(result.Message);

            result = await _blogRepository.AddRangeAsync(blogs);

            if (!result.Success)
                throw new Exception(result.Message);

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return new ExecutiveResult(ReturnMessage.Exception.ToMessage(ex.Message));
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    public async Task<ExecutiveResult<Customer>> SignInAsync(string loginName, string password)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            return new ExecutiveResult<Customer>(ReturnCode.ParamError.ToMessage("登录名不能为空白"));

        if (string.IsNullOrWhiteSpace(password))
            return new ExecutiveResult<Customer>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        var result = await _customerRepository.GetAsync(
            o => o.LoginName == loginName && o.Password == password.ToMd5()
        );

        if (result.Success)
            return new ExecutiveResult<Customer>(ReturnCode.Ok)
            {
                Data = result.Data
            };

        return new ExecutiveResult<Customer>(result.Code);
    }
}