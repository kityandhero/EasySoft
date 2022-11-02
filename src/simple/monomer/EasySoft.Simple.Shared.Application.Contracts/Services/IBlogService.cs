using EasySoft.Core.Infrastructure.Services;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Shared.Application.Contracts.Services;

public interface IBlogService : IBusinessService
{
    Task<ExecutiveResult<Blog>> GetBlogAsync(int authorId);
}