using EasySoft.Core.EntityFramework.Repositories;
using EasySoft.Simple.EntityFrameworkCore.Contexts;
using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.Simple.EntityFrameworkCore.IRepositories;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.EntityFrameworkCore.Repositories;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(DataContext context) : base(context)
    {
    }

    public async Task<ExecutiveResult<Author>> GetAuthor(int authorId)
    {
        var author = await GetDbSet().FindAsync(authorId);

        if (author == null)
        {
            return new ExecutiveResult<Author>(ReturnCode.NoData)
            {
                Data = new Author()
            };
        }

        return new ExistResult<Author>(ReturnCode.Ok)
        {
            Data = author
        };
    }

    public ExecutiveResult ExistLoginName(string loginName)
    {
        return string.IsNullOrWhiteSpace(loginName)
            ? new ExecutiveResult(ReturnCode.ParamError.ToMessage("登陆名不能为空白"))
            : Exists(o => o.LoginName == loginName);
    }

    public async Task<ExecutiveResult<Author>> CreateAsync(string loginName, string password)
    {
        if (string.IsNullOrWhiteSpace(loginName))
        {
            return new ExecutiveResult<Author>(ReturnCode.ParamError.ToMessage("登陆名不能为空白"));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return new ExecutiveResult<Author>(ReturnCode.ParamError.ToMessage("密码不能为空白"));
        }

        var author = new Author
        {
            LoginName = loginName,
            Password = password.ToMd5()
        };

        return await AddAsync(author);
    }
}