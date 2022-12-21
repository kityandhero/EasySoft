using EasySoft.Core.LogServer.Core.Controllers.Common;
using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Core.LogServer.Core.Controllers;

/// <summary>
/// ErrorLogController
/// </summary>
[Route("errorLog")]
public class ErrorLogController : AuthControllerCore
{
    private const string ControllerDescription = "错误日志管理/";

    private readonly IErrorLogService _errorLogService;

    /// <summary>
    /// ErrorLogController
    /// </summary>
    /// <param name="errorLogService"></param>
    public ErrorLogController(IErrorLogService errorLogService)
    {
        _errorLogService = errorLogService;
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="errorLogSearchDto"></param>
    /// <returns></returns>
    [Route("pageList")]
    [HttpPost]
    [Permission(ControllerDescription + "日志列表", "a2e509f1-7cdb-444e-9731-4a21bb540d2a")]
    public async Task<IApiResult> PageList(ErrorLogSearchDto errorLogSearchDto)
    {
        var result = await _errorLogService.PageListAsync(errorLogSearchDto);

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