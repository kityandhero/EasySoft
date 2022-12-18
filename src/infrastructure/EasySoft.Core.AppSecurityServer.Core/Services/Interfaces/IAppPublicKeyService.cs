using EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;
using EasySoft.UtilityTools.Standard.DataTransferObjects;

namespace EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

/// <summary>
/// 应用校验公钥服务
/// </summary>
public interface IAppPublicKeyService : IBusinessService
{
    /// <summary>
    /// 获取应用校验公钥
    /// </summary>
    /// <returns></returns>
    Task<ExecutiveResult<AppPublicKeyDto>> GetAsync();

    /// <summary>
    /// 刷新校验公钥
    /// </summary>
    /// <returns></returns>
    Task<ExecutiveResult<AppPublicKeyDto>> RefreshAsync();
}