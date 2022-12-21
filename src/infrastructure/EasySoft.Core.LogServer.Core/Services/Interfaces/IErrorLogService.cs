using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.LogServer.Core.Services.Interfaces;

/// <summary>
/// ISecurityService
/// </summary>
public interface IErrorLogService : IBusinessService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="blogSearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<ErrorLog>> PageListAsync(ErrorLogSearchDto blogSearchDto);

    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="errorLogExchange"></param>
    /// <returns></returns>
    Task SaveAsync(IErrorLogExchange errorLogExchange);
}