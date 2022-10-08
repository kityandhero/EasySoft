using EasySoft.Core.Data.Repositories;

namespace EasySoft.Simple.EntityFrameworkCore.IRepositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<ExecutiveResult<Blog>> GetBlog(int blogId);
}