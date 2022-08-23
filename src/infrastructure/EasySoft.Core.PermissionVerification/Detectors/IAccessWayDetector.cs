using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.Detectors;

public interface IAccessWayDetector
{
    public AccessWayModel? Find(string guidTag);
}