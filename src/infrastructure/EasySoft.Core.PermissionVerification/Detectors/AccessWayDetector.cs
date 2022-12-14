using EasySoft.Core.PermissionVerification.Clients;
using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Attributes;

namespace EasySoft.Core.PermissionVerification.Detectors;

/// <inheritdoc />
public class AccessWayDetector : IAccessWayDetector
{
    private readonly IPermissionClient _permissionClient;

    /// <summary>
    /// 访问探测器
    /// </summary>
    /// <param name="permissionClient"></param>
    public AccessWayDetector(IPermissionClient permissionClient)
    {
        _permissionClient = permissionClient;
    }

    /// <summary>
    /// Find
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    public async Task<IList<AccessWayModel>> Find(string guidTag)
    {
        var apiResponse = await _permissionClient.FindAccessWayModel(guidTag);

        if (!apiResponse.IsSuccessStatusCode)
            throw new UnknownException($"rpc {GetType().Name}.{nameof(Find)} call fail");

        return apiResponse.Content ?? new List<AccessWayModel>();
    }
}