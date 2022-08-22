using EasySoft.Core.IdentityVerification.Operators;
using EasySoft.UtilityTools.Standard.Competence;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.IdentityVerification.Observers;

public abstract class PermissionObserverCore : IPermissionObserver
{
    private readonly IActualOperator _actualOperator;

    protected PermissionObserverCore(IActualOperator applicationActualOperator)
    {
        _actualOperator = applicationActualOperator;
    }

    public IActualOperator GetActualOperator()
    {
        return _actualOperator;
    }

    public abstract bool OnJudging();
    public abstract List<CompetenceEntity> GetCompetenceEntityCollection();

    public virtual bool CheckAccessPermission(string guidTag)
    {
        var listCompetenceEntity = GetCompetenceEntityCollection();

        foreach (var ce in listCompetenceEntity)
        {
            if (ce.GuidTag == guidTag.Remove("-") || ce.GuidTag == ConstCollection.SuperRoleGuidTag)
            {
                return true;
            }
        }

        return false;
    }
}