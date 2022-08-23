using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.PermissionVerification.Observers;
using EasySoft.UtilityTools.Standard.Competence;

namespace WebApplicationTest.EasyTokens;

public class ApplicationPermissionObserver : PermissionObserverCore
{
    public ApplicationPermissionObserver(IActualOperator applicationActualOperator) : base(applicationActualOperator)
    {
    }

    public override bool OnJudging()
    {
        throw new NotImplementedException();
    }

    public override List<CompetenceEntity> GetCompetenceEntityCollection()
    {
        throw new NotImplementedException();
    }
}