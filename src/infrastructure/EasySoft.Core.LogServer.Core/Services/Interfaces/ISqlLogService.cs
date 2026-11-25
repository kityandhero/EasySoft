using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.UtilityTools.Core.Services;

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
    /// <param name="sqlLogTransitiveMessage"></param> 
    /// <returns></returns>
    Task SaveAsync(ISqlLogTransitiveMessage sqlLogTransitiveMessage);
}