using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Simple.Tradition.Service.Services.Interfaces;

/// <summary>
/// IBlogService
/// </summary>
public interface IBlogService : IBusinessService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="blogSearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<Blog>> PageListAsync(BlogSearchDto blogSearchDto);

    /// <summary>
    /// GetFirstAsync
    /// </summary>
    /// <returns></returns>
    public Task<ExecutiveResult<BlogDto>> GetFirstAsync();

    /// <summary>
    /// GetBlogDtoSync
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    public Task<ExecutiveResult<BlogDto>> GetBlogDtoAsync(long blogId);
}