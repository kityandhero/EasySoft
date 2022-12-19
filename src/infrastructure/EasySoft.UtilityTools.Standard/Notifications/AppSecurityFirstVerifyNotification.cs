namespace EasySoft.UtilityTools.Standard.Notifications;

/// <summary>
/// 应用安全首次校验通知
/// </summary>
public class AppSecurityFirstVerifyNotification : INotification
{
    /// <summary>
    /// VerifyResult
    /// </summary>
    public bool VerifyResult { get; private set; }

    /// <summary>
    /// 应用安全首次校验通知
    /// </summary>
    /// <param name="verifyResult"></param>
    public AppSecurityFirstVerifyNotification(
        bool verifyResult
    )
    {
        VerifyResult = verifyResult;
    }
}