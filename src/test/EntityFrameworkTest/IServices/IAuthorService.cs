using EasySoft.Core.Web.Framework.Services;
using EasySoft.UtilityTools.Standard.Result;
using EntityFrameworkTest.Entities;

namespace EntityFrameworkTest.IServices;

public interface IAuthorService : IService
{
    Task<ExecutiveResult<Author>> GetAuthor(int authorId);
}