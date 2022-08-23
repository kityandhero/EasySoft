using EasySoft.Core.AuthenticationCore.Operators;

namespace EasySoft.Core.PermissionVerification.Observers;

public abstract class PermissionObserver : PermissionObserverCore
{
    protected PermissionObserver(IActualOperator applicationActualOperator) : base(applicationActualOperator)
    {
    }
}