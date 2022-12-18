using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Controllers;

/// <summary>
/// AppSecurityController
/// </summary>
[Route("appPublicKey")]
[Operator]
public class AppPublicKeyController : CustomControllerBase
{
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
    public async Task<IApiResult> Refresh()
    {
        var result = await _appPublicKeyService.RefreshAsync();

        return this.WrapperExecutiveResult(result);
    }
}