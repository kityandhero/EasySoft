using EasySoft.Simple.Single.Application.DataTransfer;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

/// <summary>
/// Swagger文档测试
/// </summary>
public class SwaggerController : AreaControllerCore
{
    /// <summary>
    /// Test1
    /// </summary>
    /// <returns></returns>
    [Route("test1")]
    [HttpPost]
    public IApiResult Test1()
    {
        return new ApiResult(ReturnCode.Ok)
        {
            Data = new
            {
                title = "title",
                description = "description"
            }
        };
    }

    /// <summary>
    /// Test2
    /// </summary>
    /// <returns></returns>
    [Route("test2")]
    [HttpPost]
    public IApiResult<SimpleDto> Test2()
    {
        return new ApiResult<SimpleDto>(ReturnCode.Ok)
        {
            Data = new SimpleDto()
        };
    }

    /// <summary>
    /// Test3
    /// </summary>
    /// <returns></returns>
    [Route("test3")]
    [HttpPost]
    public ApiResult<SimpleDto, SimpleDto> Test3()
    {
        var result = new ApiResult<SimpleDto, SimpleDto>(ReturnCode.Ok)
        {
            Data = new SimpleDto(),
            ExtraData = new SimpleDto()
        };

        // result.SetCamelCase(false);

        return result;
    }

    /// <summary>
    /// Test4
    /// </summary>
    /// <returns></returns>
    [Route("test4")]
    [HttpPost]
    public IActionResult Test4()
    {
        return Content("success");
    }
}