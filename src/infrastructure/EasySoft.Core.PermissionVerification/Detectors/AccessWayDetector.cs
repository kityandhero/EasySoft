using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.Core.PermissionVerification.Remotes;
using Refit;

namespace EasySoft.Core.PermissionVerification.Detectors;

public class AccessWayDetector : IAccessWayDetector
{
    /// <summary>
    /// Find
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    public async Task<IList<AccessWayModel>> Find(string guidTag)
    {
        var api = RestService.For<IAccessWayApi>(GeneralConfigAssist.GetPermissionServerHostUrl());

        return await api.Find(guidTag);
    }
}