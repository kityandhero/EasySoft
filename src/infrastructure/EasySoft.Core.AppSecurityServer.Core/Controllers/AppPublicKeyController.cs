using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Controllers;

/// <summary>
/// AppSecurityController
/// </summary>
[Route("appPublicKey")]
[Operator]
public class AppPublicKeyController : CustomControllerBase
{
    private const string ControllerDescription = "应用校验密钥管理/";

    private readonly IAppPublicKeyService _appPublicKeyService;

    /// <summary>
    /// AppPublicKeyController
    /// </summary>
    /// <param name="appPublicKeyService"></param>
    public AppPublicKeyController(IAppPublicKeyService appPublicKeyService)
    {
        _appPublicKeyService = appPublicKeyService;
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <returns></returns>
    [Route("get")]
    [HttpPost]
    [Permission(ControllerDescription + "获取密钥", "f10b1036-85f0-4972-8133-64b50f29b488")]
    public async Task<IApiResult> Get()
    {
        var result = await _appPublicKeyService.GetAsync();

        return this.WrapperExecutiveResult(result);
    }

    /// <summary>
    /// Refresh  
    /// </summary>
    /// <returns></returns>
    [Route("refresh")]
    [HttpPost]
    [Permission(ControllerDescription + "刷新密钥", "2e3047c3-b578-4a89-97e6-a6c412feaa6a")]
    public async Task<IApiResult> Refresh()
    {
        var result = await _appPublicKeyService.RefreshAsync();

        return this.WrapperExecutiveResult(result);
    }
}