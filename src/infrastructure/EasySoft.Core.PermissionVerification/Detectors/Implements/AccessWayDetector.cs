using EasySoft.Core.PermissionVerification.Clients;
using EasySoft.Core.PermissionVerification.Detectors.Interfaces;
using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.PermissionVerification.Detectors.Implements;

/// <inheritdoc />
public class AccessWayDetector : IAccessWayDetector
{
    private readonly IPermissionClient _permissionClient;

    /// <summary>
    /// 构造函数
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
    public async Task<ExecutiveResult<AccessWayModel>> Find(string guidTag)
    {
        var applicationConfigureAssist = ServiceProviderAssist.Resolve<ApplicationConfigureAssist>();

        var applicationIdConfigValue = applicationConfigureAssist.GetApplicationId();

        var salt = AppSecurityAssist.GetSalt();
        var sign = AppSecurityAssist.SignRequest(
            applicationIdConfigValue.Value,
            ApplicationSecurityClientConfigure.GetPublicKey(),
            salt
        );

        var apiResponse = await _permissionClient.FindAccessWayModel(
            guidTag,
            applicationIdConfigValue.Value,
            sign,
            salt
        );

        if (!apiResponse.IsSuccessStatusCode || apiResponse.Content == null)
        {
            throw new UnknownException($"rpc {GetType().Name}.{nameof(Find)} call fail");
        }

        return apiResponse.Content.ToExecutiveResult();
    }
}