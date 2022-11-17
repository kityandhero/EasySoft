namespace EasySoft.Core.PermissionVerification.Observers;

/// <summary>
/// PermissionObserver
/// </summary>
public abstract class PermissionObserver : PermissionObserverCore
{
    /// <summary>
    /// PermissionObserver
    /// </summary>
    /// <param name="applicationActualOperator"></param>
    protected PermissionObserver(IActualOperator applicationActualOperator) : base(applicationActualOperator)
    {
    }
}