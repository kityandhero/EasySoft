using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Simple.AccountCenter.Application.Contracts.DataTransferObjects.ApiParams;
using EasySoft.Simple.AccountCenter.Application.Contracts.ExtensionMethods;
using EasySoft.Simple.AccountCenter.Application.Contracts.Services;
using EasySoft.Simple.AccountCenter.Domain.Aggregates.AccountAggregate;
using EasySoft.Simple.DomainDrivenDesign.Application.Contracts.DataTransferObjects;

namespace EasySoft.Simple.AccountCenter.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> repository)
    {
        _userRepository = repository;
    }

    public async Task<ExecutiveResult<User>> RegisterAsync(RegisterDto registerDto)
    {
        if (string.IsNullOrWhiteSpace(registerDto.LoginName))
            return new ExecutiveResult<User>(ReturnCode.ParamError.ToMessage("登陆名不能为空白"));

        if (string.IsNullOrWhiteSpace(registerDto.Password))
            return new ExecutiveResult<User>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        var result = await _userRepository.ExistAsync(o => o.LoginName == registerDto.LoginName);

        if (result.Success)
            return new ExecutiveResult<User>(ReturnCode.NoChange.ToMessage("登录名已存在"));

        var user = new User
        {
            LoginName = registerDto.LoginName,
            Password = registerDto.Password.ToMd5()
        };

        return await _userRepository.AddAsync(user);
    }

    public async Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword)
    {
        if (namePassword.Count <= 0) return new ExecutiveResult(ReturnCode.NoChange.ToMessage("无可添加数据"));

        var list = namePassword.Select(item => new User
            {
                LoginName = item.Key,
                Password = item.Value
            })
            .ToList();

        return await _userRepository.AddRangeAsync(list);
    }

    public async Task<ExecutiveResult<UserDto>> SignInAsync(SignInDto signInDto)
    {
        if (string.IsNullOrWhiteSpace(signInDto.LoginName))
            return new ExecutiveResult<UserDto>(ReturnCode.ParamError.ToMessage("登录名不能为空白"));

        if (string.IsNullOrWhiteSpace(signInDto.Password))
            return new ExecutiveResult<UserDto>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        var result = await _userRepository.GetAsync(o => o.LoginName == signInDto.Password);

        if (!result.Success || result.Data == null)
            return new ExecutiveResult<UserDto>(ReturnCode.NoData.ToMessage("用户名不存在"));

        var user = result.Data;

        if (signInDto.Password.ToMd5(ApplicationConfigure.PasswordSalt) != user.Password)
            return new ExecutiveResult<UserDto>(ReturnCode.ParamError.ToMessage("用户名或密码错误"));

        return new ExecutiveResult<UserDto>(ReturnCode.Ok)
        {
            Data = user.ToUserDto()
        };
    }
}