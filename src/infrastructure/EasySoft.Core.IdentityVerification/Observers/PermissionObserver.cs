using EasySoft.Core.IdentityVerification.Operators;

namespace EasySoft.Core.IdentityVerification.Observers;

public abstract class PermissionObserver : PermissionObserverCore
{
    protected PermissionObserver(IActualOperator applicationActualOperator) : base(applicationActualOperator)
    {
    }
}