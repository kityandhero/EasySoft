using EasySoft.Core.LogServer.Core.Controllers.Common;
using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Controllers;

/// <summary>
/// SqlExecutionRecordController
/// </summary>
[Route("sqlExecutionRecord")]
public class SqlExecutionRecordController : AuthControllerCore
{
    private const string ControllerDescription = "Sql日志管理/";

    private readonly ISqlExecutionRecordService _sqlExecutionRecordService;

    /// <summary>
    /// ErrorLogController
    /// </summary>
    /// <param name="sqlExecutionRecordService"></param>
    public SqlExecutionRecordController(ISqlExecutionRecordService sqlExecutionRecordService)
    {
        _sqlExecutionRecordService = sqlExecutionRecordService;
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="sqlExecutionRecordSearchDto"></param>
    /// <returns></returns>
    [Route("pageList")]
    [HttpPost]
    [Permission(ControllerDescription + "日志列表", "ca07db32-6086-4db4-a7bd-f852135bf539")]
    public async Task<IApiResult> PageList(SqlExecutionRecordSearchDto sqlExecutionRecordSearchDto)
    {
        var result = await _sqlExecutionRecordService.PageListAsync(sqlExecutionRecordSearchDto);

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