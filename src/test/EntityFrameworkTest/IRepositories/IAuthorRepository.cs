using EasySoft.Core.EntityFramework.InterFaces;
using EasySoft.UtilityTools.Standard.Result;
using EntityFrameworkTest.Entities;

namespace EntityFrameworkTest.IRepositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);
}