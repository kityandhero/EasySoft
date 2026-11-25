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
        var applicationConfigureAssist = ServiceProviderAssist.Resolve<ApplicationConfigureAssist>();

        var applicationIdConfigValue = applicationConfigureAssist.GetApplicationId();

        var salt = AppSecurityAssist.GetSalt();
        var sign = AppSecurityAssist.SignRequest(
            applicationIdConfigValue.Value,
            ApplicationSecurityClientConfigure.GetPublicKey(),
            salt
        );

        var apiResponse = await _permissionClient.GetCompetenceEntityCollectionAsync(
            roleGroupId,
            applicationIdConfigValue.Value,
            sign,
            salt
        );

        if (!apiResponse.IsSuccessStatusCode || apiResponse.Content == null)
        {
            throw new UnknownException($"rpc {GetType().Name}.{nameof(GetCompetenceEntityCollection)} call fail");
        }

        return apiResponse.Content.Data ?? new List<CompetenceEntity>();
    }
}