using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.WebLog.Application.Contracts.DataTransferObjects;
using EasySoft.Simple.WebLog.Application.Contracts.ExtensionMethods;
using EasySoft.Simple.WebLog.Application.Contracts.Services;
using EasySoft.Simple.WebLog.Domain.Aggregates.BlogAggregate;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.WebLog.Application.Services;

public class BlogService : IBlogService
{
    private readonly IRepository<Blog> _blogRepository;

    public BlogService(IRepository<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<ExecutiveResult<Blog>> GetBlogAsync(int blogId)
    {
        return await _blogRepository.GetAsync(blogId);
    }

    public async Task<ExecutiveResult<Blog>> GetFirstAsync()
    {
        var enumerable = await _blogRepository.SingleListAsync();

        var list = enumerable.ToList();

        if (!list.Any()) return new ExecutiveResult<Blog>(ReturnCode.NoData);

        var first = list.First();

        return new ExecutiveResult<Blog>(ReturnCode.Ok)
        {
            Data = first
        };
    }

    public async Task<ExecutiveResult<BlogDto>> GetBlogDtoAsync(int authorId)
    {
        var result = await _blogRepository.GetAsync(authorId);

        if (!result.Success) return new ExecutiveResult<BlogDto>(result.Code);

        if (result.Data != null)
            return new ExecutiveResult<BlogDto>(result.Code)
            {
                Data = result.Data.ToBlogDto()
            };

        return new ExecutiveResult<BlogDto>(ReturnCode.NoData);
    }

    public async Task<ExecutiveResult<Blog>> UpdateFirstAsync()
    {
        var result = await GetFirstAsync();

        if (!result.Success) return new ExecutiveResult<Blog>(ReturnCode.NoData);

        if (result.Data == null) return new ExecutiveResult<Blog>(ReturnCode.DataError);

        var first = result.Data;

        // var result = await _authorRepository.GetAsync(first.Id);

        first.Motto = UniqueIdAssist.CreateUUID();

        var resultUpdate = await _blogRepository.UpdateAsync(first);

        if (resultUpdate.Success)
            return new ExecutiveResult<Blog>(ReturnCode.Ok)
            {
                Data = resultUpdate.Data
            };

        return resultUpdate;
    }
}