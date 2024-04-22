using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;

namespace EasySoft.Core.LogServer.Core.Services.Interfaces;

/// <summary>
/// SqlLogService
/// </summary>
public interface ISqlLogService : IBusinessService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="sqlLogSearchDto"></param>
    /// <returns></returns>
    Task<PageListResult<ISqlLogStore>> PageListAsync(SqlLogSearchDto sqlLogSearchDto);

    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="sqlLogMessage"></param>
    /// <returns></returns>
    Task SaveAsync(ISqlLogMessage sqlLogMessage);
}