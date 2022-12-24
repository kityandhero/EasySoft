namespace EasySoft.Core.AppSecurityServer.Core.Clients;

/// <summary>
/// IPermissionClient
/// </summary>
public interface IMaintainSuperRoleClient
{
    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="originAppId">请求源应用标识</param>
    /// <param name="originAppSign">请求源签名</param>
    /// <param name="originAppSalt">请求源混淆字符串</param>
    /// <returns></returns>
    [Post("/rpc/maintainSuperRole")]
    public Task<ApiResponse<RpcResult<AppSecurityDto>>> MaintainSuperRole(
        int channel,
        [Header("originId")] string originAppId,
        [Header("originSign")] string originAppSign,
        [Header("originSalt")] string originAppSalt
    );
}