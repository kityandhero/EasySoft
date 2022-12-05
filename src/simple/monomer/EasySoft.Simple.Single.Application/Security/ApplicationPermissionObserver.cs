using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.PermissionVerification.Observers;
using EasySoft.UtilityTools.Standard.Competence;

namespace EasySoft.Simple.Single.Application.Security;

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
    public override Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync()
    {
        throw new NotImplementedException();
    }
}