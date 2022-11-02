using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.WebLog.Application.Contracts.Services;

public interface IBlogService : IBusinessService
{
    Task<ExecutiveResult<Blog>> GetBlogAsync(int authorId);
}