using EasySoft.Core.Web.Framework.Services;
using EasySoft.UtilityTools.Standard.Result;
using EntityFrameworkTest.Entities;

namespace EntityFrameworkTest.IServices;

public interface IAuthorService : IService
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);

    public Task<ExecutiveResult<Author>> RegisterAsync(string loginName, string password);

    public ExecutiveResult<Author> SignIn(string loginName, string password);
}