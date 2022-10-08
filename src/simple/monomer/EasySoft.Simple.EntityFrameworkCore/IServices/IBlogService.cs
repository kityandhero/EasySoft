using EasySoft.Core.Infrastructure.Services;

namespace EasySoft.Simple.EntityFrameworkCore.IServices;

public interface IBlogService : IService
{
    Task<ExecutiveResult<Blog>> GetBlog(int authorId);
}