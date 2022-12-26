using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Core.AppSecurityVerification.Clients;

/// <summary>
/// IPermissionClient
/// </summary>
public interface IAppSecurityClient
{
    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    [Post("/appSecurityRpc/credentialVerify")]
    public Task<ApiResponse<RpcResult<AppPublicKeyDto>>> CredentialVerifyAsync([Body] AppSecurityDto appSecurityDto);
}