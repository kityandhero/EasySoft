namespace EasySoft.Core.PermissionServer.Core.Services.Interfaces;

/// <summary>
/// access way service
/// </summary>
public interface IAccessWayService
{
    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="accessWayExchange"></param>
    /// <returns></returns>
    Task SaveAccessWayAsync(AccessWayExchange accessWayExchange);
}