using EasySoft.Core.Web.Framework.Repositories;
using EntityFrameworkTest.Entities;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IRepositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);
}