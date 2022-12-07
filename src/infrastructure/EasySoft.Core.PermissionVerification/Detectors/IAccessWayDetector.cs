using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.Detectors;

/// <summary>
/// IAccessWayDetector
/// </summary>
public interface IAccessWayDetector
{
    /// <summary>
    /// Find
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    public Task<AccessWayModel?> Find(string guidTag);
}