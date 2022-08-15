using EntityFrameworkTest.Entities;
using EasySoft.Core.Mvc.Framework.Repositories;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.IRepositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<ExecutiveResult<Blog>> GetBlog(int blogId);
}