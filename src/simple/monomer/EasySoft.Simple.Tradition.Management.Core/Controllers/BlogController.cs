using EasySoft.Simple.Tradition.Management.Core.Common;

namespace EasySoft.Simple.Tradition.Management.Core.Controllers;

/// <summary>
/// BlogController
/// </summary>
[Route("blog")]
public class BlogController : AuthControllerCore
{
    private const string ControllerDescription = "博客管理/";

    private readonly IBlogService _blogService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="blogService"></param>
    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="blogSearchDto"></param>
    /// <returns></returns>
    [Route("pageList")]
    [HttpPost]
    [Permission(ControllerDescription + "博客列表", "04b1998e-7b29-4531-bc1a-767a938e1b86")]
    public async Task<IApiResult> PageList(BlogSearchDto blogSearchDto)
    {
        var result = await _blogService.PageListAsync(blogSearchDto);

        return this.Success(
            result.List,
            new
            {
                pageNo = result.PageIndex,
                pageSize = result.PageSize,
                total = result.TotalSize
            }
        );
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="blogDto"></param>
    /// <returns></returns>
    [Route("get")]
    [HttpPost]
    [Permission(ControllerDescription + "博客详情", "16ccbcfb-15c0-4605-abd0-736f91e890af")]
    public async Task<IApiResult> Get(BlogDto blogDto)
    {
        var result = await _blogService.GetBlogDtoSync(blogDto.BlogId);

        return this.WrapperExecutiveResult(result);
    }
}