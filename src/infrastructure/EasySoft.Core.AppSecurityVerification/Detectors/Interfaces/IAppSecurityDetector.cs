namespace EasySoft.Core.AppSecurityVerification.Detectors.Interfaces;

/// <summary>
/// 应用安全探测器
/// </summary>
public interface IAppSecurityDetector
{
    /// <summary>
    /// 通道检验
    /// </summary>
    /// <returns></returns>
    public Task ChannelCheck();

    /// <summary>
    /// 凭据校验
    /// </summary>
    /// <returns></returns>
    public Task CredentialVerify();
}