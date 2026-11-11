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
    /// <param name="generalLogSearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<IGeneralLogStore>> PageListAsync(GeneralLogSearchDto generalLogSearchDto);

    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="generalLogTransitiveMessage
    /// <returns></returns>
    Task SaveAsync(IGeneralLogTransitiveMessage generalLogTransitiveMessage);
}