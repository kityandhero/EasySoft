using EasySoft.Core.IdentityVerification.Observers;
using EasySoft.Core.IdentityVerification.Operators;
using EasySoft.UtilityTools.Standard.Competence;

namespace WebApplicationTest.IdentityVerifications;

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