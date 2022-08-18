using EasySoft.Core.Web.Framework.Services;
using EntityFrameworkTest.Entities;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IServices;

public interface IBlogService : IService
{
    Task<ExecutiveResult<Blog>> GetBlog(int authorId);
}