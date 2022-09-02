using EasySoft.Core.EntityFramework.InterFaces;
using EasySoft.UtilityTools.Standard.Result;
using EntityFrameworkTest.Entities;

namespace EntityFrameworkTest.IRepositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);

    public ExecutiveResult ExistLoginName(string loginName);

    public Task<ExecutiveResult<Author>> CreateAsync(string loginName, string password);
}