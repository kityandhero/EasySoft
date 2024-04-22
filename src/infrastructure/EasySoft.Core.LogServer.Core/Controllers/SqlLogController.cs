using EasySoft.Core.LogServer.Core.Controllers.Common;
using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Core.LogServer.Core.Controllers;

/// <summary>
/// SqlLogController
/// </summary>
[Route("sqlLog")]
public class SqlLogController : AuthControllerCore
{
    private const string ControllerDescription = "Sql日志管理/";

    private readonly ISqlLogService _sqlLogService;

    /// <summary>
    /// ErrorLogController
    /// </summary>
    /// <param name="sqlLogService"></param>
    public SqlLogController(ISqlLogService sqlLogService)
    {
        _sqlLogService = sqlLogService;
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="sqlLogSearchDto"></param>
    /// <returns></returns>
    [Route("pageList")]
    [HttpPost]
    [Permission(ControllerDescription + "日志列表", "ca07db32-6086-4db4-a7bd-f852135bf539")]
    public async Task<IApiResult> PageList(SqlLogSearchDto sqlLogSearchDto)
    {
        var result = await _sqlLogService.PageListAsync(sqlLogSearchDto);

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