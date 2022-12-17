using EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Controllers;

/// <summary>
/// AppSecurityController
/// </summary>
[Route("appSecurity")]
public class AppSecurityController : CustomControllerBase
{
    private readonly IAppSecurityService _appSecurityService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="appSecurityService"></param>
    public AppSecurityController(IAppSecurityService appSecurityService)
    {
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
        var result = await _appSecurityService.VerifyAsync(appSecurityDto);

        if (!result.Success || result.Data == null) return new List<AppSecurityDto>();

        return new List<AppSecurityDto>
        {
            result.Data
        };
    }
}