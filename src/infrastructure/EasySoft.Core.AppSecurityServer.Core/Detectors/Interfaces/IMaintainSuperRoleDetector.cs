using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.Core.AppSecurityServer.Core.Detectors.Interfaces;

/// <summary>
/// 应用安全探测器
/// </summary>
public interface IMaintainSuperRoleDetector
{
    /// <summary>
    /// 安全校验
    /// </summary>
    /// <returns></returns>
    public Task MaintainSuperRole();
}