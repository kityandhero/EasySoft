using EasySoft.Core.EntityFramework.InterFaces;
using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.EntityFrameworkCore.IRepositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<ExecutiveResult<Blog>> GetBlog(int blogId);
}