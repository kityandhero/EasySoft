namespace EasySoft.UtilityTools.Standard.Notifications;

/// <summary>
/// 应用安全校验通知
/// </summary>
public class PermissionAccessNotification : INotification
{
    /// <summary>
    /// VerifyResult
    /// </summary>
    public IAccessWayPersistence AccessWayPersistence { get; }

    /// <summary>
    /// 应用安全首次校验通知
    /// </summary>
    /// <param name="accessWayPersistence"></param>
    public PermissionAccessNotification(
        IAccessWayPersistence accessWayPersistence
    )
    {
        AccessWayPersistence = accessWayPersistence;
    }
}