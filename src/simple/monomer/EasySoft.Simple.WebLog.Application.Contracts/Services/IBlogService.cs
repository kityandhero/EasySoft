using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.WebLog.Application.Contracts.DataTransferObjects;
using EasySoft.Simple.WebLog.Domain.Aggregates.BlogAggregate;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.WebLog.Application.Contracts.Services;

public interface IBlogService : IBusinessService
{
    Task<ExecutiveResult<Blog>> GetBlogAsync(int authorId);

    public Task<ExecutiveResult<Blog>> GetFirstAsync();

    public Task<ExecutiveResult<BlogDto>> GetBlogDtoAsync(int authorId);

    public Task<ExecutiveResult<Blog>> UpdateFirstAsync();
}