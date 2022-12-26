namespace EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

/// <summary>
/// 应用密钥服务
/// </summary>
public interface IAppSecurityService : IBusinessService
{
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    Task<ExecutiveResult<AppSecurityDto>> CreateAsync(AppSecurityDto appSecurityDto);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    Task<ExecutiveResult> TryCreateAsync(int channel);

    /// <summary>
    /// 获取应用安全主控端, 即AppSecurityServer应用对应的应用配置
    /// </summary>
    /// <returns></returns>
    Task<ExecutiveResult<AppSecurityDto>> GetMainControlAppSecurity();

    /// <summary>
    /// SingleListNeedMaintain
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<AppSecurityDto>> SingleListNeedMaintain();

    /// <summary>
    /// 维护主控配置, 在数据丢失时自动维护
    /// </summary>
    /// <returns></returns>
    Task<ExecutiveResult> MaintainMasterControlAsync();

    /// <summary>
    /// SetSuperRoleNextMaintainTime
    /// </summary>
    /// <returns></returns>
    Task SetSuperNextMaintainTime(AppSecurityDto appSecurityDto);
}