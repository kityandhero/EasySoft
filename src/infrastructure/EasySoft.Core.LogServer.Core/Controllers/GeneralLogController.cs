using EasySoft.Core.LogServer.Core.Controllers.Common;
using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Controllers;

/// <summary>
/// GeneralLogController
/// </summary>
[Route("generalLog")]
public class GeneralLogController : AuthControllerCore
{
    private const string ControllerDescription = "一般日志管理/";

    private readonly IGeneralLogService _generalLogService;

    /// <summary>
    /// ErrorLogController
    /// </summary>
    /// <param name="generalLogService"></param>
    public GeneralLogController(IGeneralLogService generalLogService)
    {
        _generalLogService = generalLogService;
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="generalLogSearchDto"></param>
    /// <returns></returns>
    [Route("pageList")]
    [HttpPost]
    [Permission(ControllerDescription + "日志列表", "b9a41ef3-7a4e-4294-94a5-4a618d180da5")]
    public async Task<IApiResult> PageList(GeneralLogSearchDto generalLogSearchDto)
    {
        var result = await _generalLogService.PageListAsync(generalLogSearchDto);

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