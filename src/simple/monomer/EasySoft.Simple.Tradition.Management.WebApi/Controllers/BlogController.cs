using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Management.WebApi.Common;

namespace EasySoft.Simple.Tradition.Management.WebApi.Controllers;

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
    [Description(ControllerDescription + "博客列表")]
    [GuidTag("04b1998e-7b29-4531-bc1a-767a938e1b86")]
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
}