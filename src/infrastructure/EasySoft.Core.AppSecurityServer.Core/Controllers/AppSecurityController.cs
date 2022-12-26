using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Controllers;

/// <summary>
/// AppSecurityController
/// </summary>
[Route("appSecurity")]
public class AppSecurityController : CustomControllerBase
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IWebHostEnvironment _environment;
    private readonly IAppPublicKeyService _appPublicKeyService;
    private readonly IAppSecurityService _appSecurityService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    /// <param name="appPublicKeyService"></param>
    /// <param name="appSecurityService"></param>
    public AppSecurityController(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IAppPublicKeyService appPublicKeyService,
        IAppSecurityService appSecurityService
    )
    {
        _loggerFactory = loggerFactory;
        _environment = environment;
        _appPublicKeyService = appPublicKeyService;
        _appSecurityService = appSecurityService;
    }
}