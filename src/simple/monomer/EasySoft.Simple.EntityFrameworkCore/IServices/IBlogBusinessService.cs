using EasySoft.Core.Infrastructure.Services;

namespace EasySoft.Simple.EntityFrameworkCore.IServices;

public interface IBlogBusinessService : IBusinessService
{
    Task<ExecutiveResult<Blog>> GetBlogAsync(int authorId);
}