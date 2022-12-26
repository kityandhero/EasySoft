using EasySoft.Core.Data.Attributes;

namespace EasySoft.Core.PermissionServer.Core.Services.Interfaces;

/// <summary>
/// ISecurityService
/// </summary>
public interface IPermissionRpcService : IBusinessService
{
    /// <summary>
    /// get competence entity collection
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync(long userId);

    /// <summary>
    /// get competence entity collection
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    Task<ExecutiveResult<AccessWayModel>> FindAccessWayAsync(string guidTag);

    /// <summary>
    /// 维护超级管理角色以及角色组
    /// </summary>
    /// <returns></returns>
    [UnitOfWork]
    Task MaintainSuper(int channel);
}