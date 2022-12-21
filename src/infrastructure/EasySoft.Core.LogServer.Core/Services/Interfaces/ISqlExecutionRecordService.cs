using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.LogServer.Core.Services.Interfaces;

/// <summary>
/// SqlExecutionRecordService
/// </summary>
public interface ISqlExecutionRecordService : IBusinessService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="sqlExecutionRecordSearchDto"></param>
    /// <returns></returns>
    Task<PageListResult<SqlExecutionRecord>> PageListAsync(SqlExecutionRecordSearchDto sqlExecutionRecordSearchDto);

    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="sqlExecutionRecord"></param>
    /// <returns></returns>
    Task SaveAsync(ISqlExecutionRecordExchange sqlExecutionRecord);
}