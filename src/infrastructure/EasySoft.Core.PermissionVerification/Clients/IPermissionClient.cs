using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Core.Results.Implements;
using Refit;

namespace EasySoft.Core.PermissionVerification.Clients;

/// <summary>
/// IPermissionClient
/// </summary>
public interface IPermissionClient
{
    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="guidTag"></param>
    /// <param name="originAppId">请求源应用标识</param>
    /// <param name="originAppSign">请求源签名</param>
    /// <param name="originAppSalt">请求源混淆字符串</param>
    /// <returns></returns>
    [Post("/accessWay/find")]
    public Task<ApiResponse<RpcResult<AccessWayModel>>> FindAccessWayModel(
        string guidTag,
        [Header("originId")] string originAppId,
        [Header("originSign")] string originAppSign,
        [Header("originSalt")] string originAppSalt
    );

    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="roleGroupId"></param>
    /// <param name="originAppId">请求源应用标识</param>
    /// <param name="originAppSign">请求源签名</param>
    /// <param name="originAppSalt">请求源混淆字符串</param>
    /// <returns></returns>
    [Post("/competenceEntity/getCollection")]
    Task<ApiResponse<RpcResult<List<CompetenceEntity>>>> GetCompetenceEntityCollectionAsync(
        long roleGroupId,
        [Header("originId")] string originAppId,
        [Header("originSign")] string originAppSign,
        [Header("originSalt")] string originAppSalt
    );
}