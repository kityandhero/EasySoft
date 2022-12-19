using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.UtilityTools.Standard.Notifications;

/// <summary>
/// 应用安全校验通知
/// </summary>
public class PermissionAccessNotification : INotification
{
    /// <summary>
    /// VerifyResult
    /// </summary>
    public IAccessWay AccessWay { get; }

    /// <summary>
    /// 应用安全首次校验通知
    /// </summary>
    /// <param name="accessWay"></param>
    public PermissionAccessNotification(
        IAccessWay accessWay
    )
    {
        AccessWay = accessWay;
    }
}