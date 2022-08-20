using EasySoft.Core.IdentityVerification.Entities;
using EasySoft.Core.IdentityVerification.Filters;

namespace EasySoft.Core.IdentityVerification.Detectors;

public interface IAccessWayDetector
{
    public AccessWayModel? Find(string guidTag);
}