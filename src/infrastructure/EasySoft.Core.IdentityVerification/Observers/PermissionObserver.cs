using EasySoft.Core.IdentityVerification.Operators;

namespace EasySoft.Core.IdentityVerification.Observers;

public abstract class PermissionObserver : PermissionObserverCore
{
    protected PermissionObserver(IOperator applicationOperator) : base(applicationOperator)
    {
    }
}