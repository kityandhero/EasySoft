namespace EasySoft.Core.Permission.Server.Observers;

/// <summary>
/// ApplicationPermissionObserver
/// </summary>
public class ApplicationPermissionObserver : PermissionObserverCore
{
    private readonly ISecurityService _securityService;

    /// <summary>
    /// ApplicationPermissionObserver
    /// </summary>
    /// <param name="applicationActualOperator"></param>
    /// <param name="securityService"></param>
    public ApplicationPermissionObserver(
        IActualOperator applicationActualOperator,
        ISecurityService securityService
    ) : base(applicationActualOperator)
    {
        _securityService = securityService;
    }

    /// <summary>
    /// OnJudging
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override bool OnJudging()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// GetCompetenceEntityCollection
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override async Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync()
    {
        var identification = GetActualOperator().GetIdentification();

        if (identification == null || !identification.IsInt64(out var id)) return new List<CompetenceEntity>();

        return await _securityService.GetCompetenceEntityCollectionAsync(id);
    }
}