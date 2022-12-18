using EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Controllers;

/// <summary>
/// AppSecurityController
/// </summary>
[Route("appSecurity")]
public class AppSecurityController : CustomControllerBase
{
    private readonly IAppPublicKeyService _appPublicKeyService;
    private readonly IAppSecurityService _appSecurityService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="appPublicKeyService"></param>
    /// <param name="appSecurityService"></param>
    public AppSecurityController(
        IAppPublicKeyService appPublicKeyService,
        IAppSecurityService appSecurityService
    )
    {
        _appPublicKeyService = appPublicKeyService;
        _appSecurityService = appSecurityService;
    }

    /// <summary>
    /// Verify
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    [Route("verify")]
    [HttpPost]
    public async Task<IList<AppSecurityDto>> Verify(AppSecurityDto appSecurityDto)
    {
        var resultVerify = await _appSecurityService.VerifyAsync(appSecurityDto);

        if (!resultVerify.Success || resultVerify.Data == null) return new List<AppSecurityDto>();

        var data = resultVerify.Data;

        var resultGetAppPublicKey = await _appPublicKeyService.GetAsync();

        if (!resultGetAppPublicKey.Success || resultGetAppPublicKey.Data == null) return new List<AppSecurityDto>();

        data.PublicKey = resultGetAppPublicKey.Data.Key;

        return new List<AppSecurityDto>
        {
            data
        };
    }
}