using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.Simple.EntityFrameworkCore.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.IRepositories;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using EasySoft.Simple.Shared.DataTransfer;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.EntityFrameworkCore.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<ExecutiveResult<AuthorDto>> GetAuthorDtoSync(int authorId)
    {
        var result = await _authorRepository.GetAuthor(authorId);

        if (!result.Success)
        {
            return new ExecutiveResult<AuthorDto>(result.Code);
        }

        if (result.Data != null)
        {
            return new ExecutiveResult<AuthorDto>(result.Code)
            {
                Data = result.Data.ToAuthorDto()
            };
        }

        return new ExecutiveResult<AuthorDto>(ReturnCode.NoData);
    }

    public async Task<ExecutiveResult<Author>> RegisterAsync(string loginName, string password)
    {
        if (_authorRepository.ExistLoginName(loginName).Success)
        {
            return new ExecutiveResult<Author>(ReturnCode.NoChange.ToMessage("登录名已存在"));
        }

        return await _authorRepository.CreateAsync(loginName, password);
    }

    public ExecutiveResult<Author> SignIn(string loginName, string password)
    {
        if (string.IsNullOrWhiteSpace(loginName))
        {
            return new ExecutiveResult<Author>(ReturnCode.ParamError.ToMessage("登录名不能为空白"));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return new ExecutiveResult<Author>(ReturnCode.ParamError.ToMessage("密码不能为空白"));
        }

        return _authorRepository.Get(o => o.LoginName == loginName && o.Password == password.ToMd5());
    }
}