using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Data.ExtensionMethods;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

/// <summary>
/// BlogService
/// </summary>
public class BlogService : IBlogService
{
    private readonly IRepository<Blog> _blogRepository;

    /// <summary>
    /// BlogService
    /// </summary>
    /// <param name="blogRepository"></param>
    public BlogService(IRepository<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
    }

    /// <summary>
    /// GetBlogAsync
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    public async Task<ExecutiveResult<Blog>> GetBlogAsync(int blogId)
    {
        return await _blogRepository.GetAsync(blogId);
    }

    /// <summary>
    /// GetFirstAsync
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// GetBlogDtoSync
    /// </summary>
    /// <param name="authorId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// UpdateFirst
    /// </summary>
    /// <returns></returns>
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