﻿namespace EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

/// <summary>
/// app security rpc service 应用安全远程过程调用服务 
/// </summary>
public interface IAppSecurityRpcService : IBusinessService
{
    /// <summary>
    /// 安全凭据校验
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    Task<ExecutiveResult<AppPublicKeyDto>> CredentialVerifyAsync(AppSecurityDto appSecurityDto);
}