using EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;
using EasySoft.UtilityTools.Standard.Result.Implements;

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
    /// 创建
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    Task<ExecutiveResult<AppSecurityDto>> CreateAsync(AppSecurityDto appSecurityDto);
}