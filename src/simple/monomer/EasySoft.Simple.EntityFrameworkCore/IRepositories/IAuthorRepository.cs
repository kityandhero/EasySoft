using EasySoft.Core.Data.Repositories;

namespace EasySoft.Simple.EntityFrameworkCore.IRepositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);

    public ExecutiveResult ExistLoginName(string loginName);

    public Task<ExecutiveResult<Author>> CreateAsync(string loginName, string password);
}