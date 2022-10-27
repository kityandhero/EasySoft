using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.EntityFrameworkCore.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Simple.EntityFrameworkCore.Services;

public class AuthorBusinessService : IAuthorBusinessService
{
    private readonly IRepository<Author> _authorRepository;

    public AuthorBusinessService(IRepository<Author> repository)
    {
        _authorRepository = repository;
    }

    public async Task<ExecutiveResult<AuthorDto>> GetAuthorDtoSync(int authorId)
    {
        var result = await _authorRepository.GetAsync(authorId);

        if (!result.Success) return new ExecutiveResult<AuthorDto>(result.Code);

        if (result.Data != null)
            return new ExecutiveResult<AuthorDto>(result.Code)
            {
                Data = result.Data.ToAuthorDto()
            };

        return new ExecutiveResult<AuthorDto>(ReturnCode.NoData);
    }

    public async Task<ExecutiveResult<Author>> RegisterAsync(string loginName, string password)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            return new ExecutiveResult<Author>(ReturnCode.ParamError.ToMessage("登陆名不能为空白"));

        if (string.IsNullOrWhiteSpace(password))
            return new ExecutiveResult<Author>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        var result = await _authorRepository.ExistAsync(o => o.LoginName == loginName);

        if (result.Success)
            return new ExecutiveResult<Author>(ReturnCode.NoChange.ToMessage("登录名已存在"));

        var author = new Author
        {
            LoginName = loginName,
            Password = password.ToMd5()
        };

        return await _authorRepository.AddAsync(author);
    }

    public async Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword)
    {
        if (namePassword.Count <= 0) return new ExecutiveResult(ReturnCode.NoChange.ToMessage("无可添加数据"));

        var list = new List<Author>();

        foreach (var item in namePassword)
        {
            var author = new Author
            {
                LoginName = item.Key,
                Password = item.Value
            };

            list.Add(author);
        }

        return await _authorRepository.AddRangeAsync(list);
    }

    public async Task<ExecutiveResult<Author>> SignInAsync(string loginName, string password)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            return new ExecutiveResult<Author>(ReturnCode.ParamError.ToMessage("登录名不能为空白"));

        if (string.IsNullOrWhiteSpace(password))
            return new ExecutiveResult<Author>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        return await _authorRepository.GetAsync(o => o.LoginName == loginName && o.Password == password.ToMd5());
    }
}