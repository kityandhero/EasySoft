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
    [Post("/appSecurity/verify")]
    public Task<ApiResponse<IList<AppPublicKeyDto>>> VerifyAsync(AppSecurityDto appSecurityDto);
}