using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;

namespace EasySoft.Core.LogServer.Core.Services.Interfaces;

/// <summary>
/// IGeneralLogService
/// </summary>
public interface IGeneralLogService : IBusinessService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="blogSearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<GeneralLog>> PageListAsync(GeneralLogSearchDto blogSearchDto);

    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="generalLogExchange"></param>
    /// <returns></returns>
    Task SaveAsync(IGeneralLogExchange generalLogExchange);
}