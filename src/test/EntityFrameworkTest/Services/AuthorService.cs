using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;
using EntityFrameworkTest.Entities;
using EntityFrameworkTest.IRepositories;
using EntityFrameworkTest.IServices;

namespace EntityFrameworkTest.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public Task<ExecutiveResult<Author>> GetAuthor(int authorId)
    {
        return _authorRepository.GetAuthor(authorId);
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