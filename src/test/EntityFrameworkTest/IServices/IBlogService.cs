using EntityFrameworkTest.Entities;
using Framework.Services;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IServices;

public interface IBlogService : IService
{
    Task<ExecutiveResult<Blog>> GetBlog(int authorId);
}