using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Controllers;

/// <summary>
/// AppSecurityController
/// </summary>
[Route("appSecurity")]
public class AppSecurityController : CustomControllerBase
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IAppPublicKeyService _appPublicKeyService;
    private readonly IAppSecurityService _appSecurityService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="appPublicKeyService"></param>
    /// <param name="appSecurityService"></param>
    public AppSecurityController(
        ILoggerFactory loggerFactory,
        IAppPublicKeyService appPublicKeyService,
        IAppSecurityService appSecurityService
    )
    {
        _loggerFactory = loggerFactory;
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
    public async Task<IList<AppSecurityDto>> Verify([FromBody] AppSecurityDto appSecurityDto)
    {
        try
        {
            var resultVerify = await _appSecurityService.VerifyAsync(appSecurityDto);

            if (!resultVerify.Success)
            {
                _loggerFactory.CreateLogger<object>().LogAdvanceError(resultVerify.Message);

                return new List<AppSecurityDto>();
            }

            if (resultVerify.Data == null)
            {
                _loggerFactory.CreateLogger<object>().LogAdvanceError(
                    $" appid {appSecurityDto.AppId} do not exist."
                );

                return new List<AppSecurityDto>();
            }

            var data = resultVerify.Data;

            var resultGetAppPublicKey = await _appPublicKeyService.GetAsync();

            if (!resultGetAppPublicKey.Success || resultGetAppPublicKey.Data == null) return new List<AppSecurityDto>();

            data.PublicKey = resultGetAppPublicKey.Data.Key;

            return new List<AppSecurityDto>
            {
                data
            };
        }
        catch (Exception e)
        {
            _loggerFactory.CreateLogger<object>().LogAdvanceException(e);

            throw;
        }
    }
}