using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.AccountCenter.Application.Contracts.Services;
using EasySoft.Simple.AccountCenter.Domain.Aggregates.AccountAggregate;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.AccountCenter.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _customerRepository;

    public UserService(IRepository<User> repository)
    {
        _customerRepository = repository;
    }

    public async Task<ExecutiveResult<User>> RegisterAsync(string loginName, string password)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            return new ExecutiveResult<User>(ReturnCode.ParamError.ToMessage("登陆名不能为空白"));

        if (string.IsNullOrWhiteSpace(password))
            return new ExecutiveResult<User>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        var result = await _customerRepository.ExistAsync(o => o.LoginName == loginName);

        if (result.Success)
            return new ExecutiveResult<User>(ReturnCode.NoChange.ToMessage("登录名已存在"));

        var customer = new User
        {
            LoginName = loginName,
            Password = password.ToMd5()
        };

        return await _customerRepository.AddAsync(customer);
    }

    public async Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword)
    {
        if (namePassword.Count <= 0) return new ExecutiveResult(ReturnCode.NoChange.ToMessage("无可添加数据"));

        var list = new List<User>();

        foreach (var item in namePassword)
        {
            var customer = new User
            {
                LoginName = item.Key,
                Password = item.Value
            };

            list.Add(customer);
        }

        return await _customerRepository.AddRangeAsync(list);
    }

    public async Task<ExecutiveResult<User>> SignInAsync(string loginName, string password)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            return new ExecutiveResult<User>(ReturnCode.ParamError.ToMessage("登录名不能为空白"));

        if (string.IsNullOrWhiteSpace(password))
            return new ExecutiveResult<User>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        return await _customerRepository.GetAsync(o => o.LoginName == loginName && o.Password == password.ToMd5());
    }
}