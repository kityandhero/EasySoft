using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.Domain.Aggregates.AuthorAggregate;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Application.Contracts.Services;

public interface IBlogService : IBusinessService
{
    Task<ExecutiveResult<Blog>> GetBlogAsync(int authorId);
}