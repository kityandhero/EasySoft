using EasySoft.Core.PermissionVerification.Clients;
using EasySoft.Core.PermissionVerification.Detectors.Interfaces;

namespace EasySoft.Core.PermissionVerification.Detectors.Implements;

/// <inheritdoc />
public class CompetenceDetector : ICompetenceDetector
{
    private readonly IPermissionClient _permissionClient;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="permissionClient"></param>
    public CompetenceDetector(IPermissionClient permissionClient)
    {
        _permissionClient = permissionClient;
    }

    /// <inheritdoc />
    public async Task<IList<CompetenceEntity>> GetCompetenceEntityCollection(long roleGroupId)
    {
        var appId = GeneralConfigAssist.GetAppId();
        var salt = AppSecurityAssist.GetSalt();
        var sign = AppSecurityAssist.SignRequest(
            appId,
            AppSecurityClientConfigure.GetPublicKey(),
            salt
        );

        var apiResponse = await _permissionClient.GetCompetenceEntityCollectionAsync(
            roleGroupId,
            appId,
            sign,
            salt
        );

        if (!apiResponse.IsSuccessStatusCode || apiResponse.Content == null)
            throw new UnknownException($"rpc {GetType().Name}.{nameof(GetCompetenceEntityCollection)} call fail");

        return apiResponse.Content.Data ?? new List<CompetenceEntity>();
    }
}