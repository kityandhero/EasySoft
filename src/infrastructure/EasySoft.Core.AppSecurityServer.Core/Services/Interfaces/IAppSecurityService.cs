namespace EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

/// <summary>
/// 应用密钥服务
/// </summary>
public interface IAppSecurityService : IBusinessService
{
    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    Task<ExecutiveResult<AppSecurityDto>> VerifyAsync(AppSecurityDto appSecurityDto);

    /// <summary>
    /// 维护主控配置
    /// </summary>
    /// <returns></returns>
    Task<ExecutiveResult> MaintainMasterControlAsync();

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    Task<ExecutiveResult<AppSecurityDto>> CreateAsync(AppSecurityDto appSecurityDto);
}