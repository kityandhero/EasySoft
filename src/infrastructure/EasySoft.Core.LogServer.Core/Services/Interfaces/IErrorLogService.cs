using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;

namespace EasySoft.Core.LogServer.Core.Services.Interfaces;

/// <summary>
/// ISecurityService
/// </summary>
public interface IErrorLogService : IBusinessService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="errorLogSearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<IErrorLogStore>> PageListAsync(ErrorLogSearchDto errorLogSearchDto);

    /// <summary>   
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="errorLogMessage"></param>
    /// <returns></returns>
    Task SaveAsync(IErrorLogMessage errorLogMessage);
}