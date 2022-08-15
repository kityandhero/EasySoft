using EntityFrameworkTest.Entities;
using EasySoft.Core.Mvc.Framework.Repositories;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IRepositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);
}