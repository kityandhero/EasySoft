namespace EasySoft.Core.PermissionServer.Core.Services.Interfaces;

/// <summary>
/// ISecurityService
/// </summary>
public interface IRpcService : IBusinessService
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
    /// maintain super role
    /// </summary>
    /// <returns></returns>
    Task MaintainSuperRole(int channel);
}