namespace EasySoft.Core.Permission.Server.Services.Interfaces;

/// <summary>
/// ISecurityService
/// </summary>
public interface ISecurityService
{
    /// <summary>
    /// get competence entity collection
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync(long userId);
}