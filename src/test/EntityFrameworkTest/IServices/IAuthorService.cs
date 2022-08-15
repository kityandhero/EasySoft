using EntityFrameworkTest.Entities;
using Framework.Services;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IServices;

public interface IAuthorService : IService
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);
}