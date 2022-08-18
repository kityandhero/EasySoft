using EasySoft.Core.Web.Framework.Repositories;
using EntityFrameworkTest.Entities;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IRepositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<ExecutiveResult<Blog>> GetBlog(int blogId);
}