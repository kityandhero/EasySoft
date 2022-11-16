using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;

namespace EasySoft.Simple.Tradition.Service.Services.Interfaces;

/// <summary>
/// IBlogService
/// </summary>
public interface IBlogService : IBusinessService
{
    /// <summary>
    /// GetBlogAsync
    /// </summary>
    /// <param name="authorId"></param>
    /// <returns></returns>
    Task<ExecutiveResult<Blog>> GetBlogAsync(int authorId);

    /// <summary>
    /// GetFirstAsync
    /// </summary>
    /// <returns></returns>
    public Task<ExecutiveResult<Blog>> GetFirstAsync();

    /// <summary>
    /// GetBlogDtoSync
    /// </summary>
    /// <param name="authorId"></param>
    /// <returns></returns>
    public Task<ExecutiveResult<BlogDto>> GetBlogDtoSync(int authorId);

    /// <summary>
    /// UpdateFirst
    /// </summary>
    /// <returns></returns>
    public Task<ExecutiveResult<Blog>> UpdateFirst();
}