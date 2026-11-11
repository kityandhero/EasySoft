using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Notifications;

/// <summary>
/// 应用安全校验通知
/// </summary>
public class PermissionAccessNotification : INotification
{
    /// <summary>
    /// VerifyResult
    /// </summary>
    public IAccessWayTransitiveMessage AccessWay { get; }

    /// <summary>
    /// 应用安全首次校验通知
    /// </summary>
    /// <param name="accessWay"></param>
    public PermissionAccessNotification(
        IAccessWayTransitiveMessage accessWay
    )
    {
        AccessWay = accessWay;
    }
}