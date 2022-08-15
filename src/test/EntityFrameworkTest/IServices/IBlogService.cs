using EntityFrameworkTest.Entities;
using EasySoft.Core.Mvc.Framework.Services;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IServices;

public interface IBlogService : IService
{
    Task<ExecutiveResult<Blog>> GetBlog(int authorId);
}