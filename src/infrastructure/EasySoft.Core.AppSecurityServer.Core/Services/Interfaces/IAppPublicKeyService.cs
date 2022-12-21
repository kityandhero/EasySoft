using EasySoft.UtilityTools.Standard.Result.Implements;

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

    /// <summary>
    /// 检测公钥, 用于内部数据完善
    /// </summary>
    /// <returns></returns>
    Task DetectionAsync();
}