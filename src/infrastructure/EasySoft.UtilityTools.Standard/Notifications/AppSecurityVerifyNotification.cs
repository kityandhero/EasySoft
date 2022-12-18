namespace EasySoft.UtilityTools.Standard.Notifications;

/// <summary>
/// 应用安全校验通知
/// </summary>
public class AppSecurityVerifyNotification : INotification
{
    /// <summary>
    /// VerifyResult
    /// </summary>
    public bool VerifyResult { get; private set; }

    /// <summary>
    /// 应用安全校验通知
    /// </summary>
    /// <param name="verifyResult"></param>
    public AppSecurityVerifyNotification(bool verifyResult)
    {
        VerifyResult = verifyResult;
    }
}