using EasySoft.Core.Web.Framework.Services;
using EasySoft.UtilityTools.Standard.Result;
using EntityFrameworkTest.Entities;

namespace EntityFrameworkTest.IServices;

public interface IBlogService : IService
{
    Task<ExecutiveResult<Blog>> GetBlog(int authorId);
}