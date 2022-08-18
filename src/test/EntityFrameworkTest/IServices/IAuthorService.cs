using EasySoft.Core.Web.Framework.Services;
using EntityFrameworkTest.Entities;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IServices;

public interface IAuthorService : IService
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);
}