using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Data.ExtensionMethods;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

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

    /// <inheritdoc />
    public async Task<PageListResult<Blog>> PageListAsync(BlogSearchDto blogSearchDto)
    {
        return await _blogRepository.PageListAsync<Blog>(
            blogSearchDto.PageNo,
            blogSearchDto.PageSize
        );
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<BlogDto>> GetFirstAsync()
    {
        var enumerable = await _blogRepository.SingleListAsync();

        var list = enumerable.ToList();

        if (!list.Any()) return new ExecutiveResult<BlogDto>(ReturnCode.NoData);

        var first = list.First();

        return new ExecutiveResult<BlogDto>(ReturnCode.Ok)
        {
            Data = first.ToBlogDto()
        };
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<BlogDto>> GetBlogDtoSync(long blogId)
    {
        var result = await _blogRepository.GetAsync(blogId);

        if (!result.Success) return new ExecutiveResult<BlogDto>(result.Code);

        if (result.Data != null)
            return new ExecutiveResult<BlogDto>(result.Code)
            {
                Data = result.Data.ToBlogDto()
            };

        return new ExecutiveResult<BlogDto>(ReturnCode.NoData);
    }
}