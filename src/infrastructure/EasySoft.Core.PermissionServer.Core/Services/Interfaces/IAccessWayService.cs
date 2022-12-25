using EasySoft.Core.PermissionServer.Core.DataTransferObjects;

namespace EasySoft.Core.PermissionServer.Core.Services.Interfaces;

/// <summary>
/// access way service
/// </summary>
public interface IAccessWayService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="accessWaySearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<AccessWayDto>> PageListAsync(AccessWaySearchDto accessWaySearchDto);

    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="accessWayExchange"></param>
    /// <returns></returns>
    Task SaveAccessWayAsync(AccessWayExchange accessWayExchange);
}