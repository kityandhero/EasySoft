using EasySoft.Core.EntityFramework.InterFaces;
using EasySoft.UtilityTools.Standard.Result;
using EntityFrameworkTest.Entities;

namespace EntityFrameworkTest.IRepositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<ExecutiveResult<Blog>> GetBlog(int blogId);
}