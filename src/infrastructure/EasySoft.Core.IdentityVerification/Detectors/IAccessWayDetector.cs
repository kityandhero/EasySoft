using EasySoft.Core.IdentityVerification.Entities;

namespace EasySoft.Core.IdentityVerification.Detectors;

public interface IAccessWayDetector
{
    public AccessWayModel? Find(string guidTag);
}