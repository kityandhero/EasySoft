using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Data.ExtensionMethods;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

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

    public async Task<ExecutiveResult<BlogDto>> GetBlogDtoSync(int authorId)
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

    public async Task<ExecutiveResult<Blog>> UpdateFirst()
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