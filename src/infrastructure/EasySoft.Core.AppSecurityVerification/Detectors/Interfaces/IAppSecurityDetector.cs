namespace EasySoft.Core.AppSecurityVerification.Detectors.Interfaces;

/// <summary>
/// 应用安全探测器
/// </summary>
public interface IAppSecurityDetector
{
    /// <summary>
    /// 安全校验
    /// </summary>
    /// <returns></returns>
    [LogRecord]
    public Task Verify();
}