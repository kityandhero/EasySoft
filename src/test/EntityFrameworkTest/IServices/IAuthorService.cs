using EntityFrameworkTest.Entities;
using EasySoft.Core.Mvc.Framework.Services;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IServices;

public interface IAuthorService : IService
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);
}