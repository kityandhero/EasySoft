using EasySoft.Core.Permission.Server.Entities;
using EasySoft.Core.Permission.Server.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.Permission.Server.Detectors;

public class AccessWayDetector : IAccessWayDetector
{
    private readonly IRepository<AccessWay> _accessWayRepository;

    public AccessWayDetector(
        IRepository<AccessWay> accessWayRepository
    )
    {
        _accessWayRepository = accessWayRepository;
    }

    public async Task<AccessWayModel?> Find(string guidTag)
    {
        var result = await _accessWayRepository.GetAsync(o => o.GuidTag == guidTag);

        if (!result.Success) return null;

        return result.Data?.ToAccessWayModel();
    }
}