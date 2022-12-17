using EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;

namespace EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

/// <summary>
/// IAppSecurityService
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
    /// 创建
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    Task<ExecutiveResult<AppSecurityDto>> CreateAsync(AppSecurityDto appSecurityDto);
}