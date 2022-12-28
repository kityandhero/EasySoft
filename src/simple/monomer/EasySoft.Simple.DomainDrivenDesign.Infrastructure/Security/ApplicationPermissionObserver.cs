namespace EasySoft.Simple.DomainDrivenDesign.Infrastructure.Security;

/// <summary>
/// ApplicationPermissionObserver
/// </summary>
public class ApplicationPermissionObserver : PermissionObserverCore
{
    /// <summary>
    /// ApplicationPermissionObserver
    /// </summary>
    /// <param name="applicationActualOperator"></param>
    public ApplicationPermissionObserver(IActualOperator applicationActualOperator) : base(applicationActualOperator)
    {
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
    public override Task<IList<CompetenceEntity>> GetCompetenceEntityCollectionAsync(string identity)
    {
        throw new NotImplementedException();
    }
}