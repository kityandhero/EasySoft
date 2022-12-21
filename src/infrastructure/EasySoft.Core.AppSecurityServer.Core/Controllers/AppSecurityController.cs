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

    /// <summary>
    /// Verify
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    [Route("verify")]
    [HttpPost]
    public async Task<RpcResult<AppPublicKeyDto>> Verify([FromBody] AppSecurityDto appSecurityDto)
    {
        try
        {
            var resultVerify = await _appSecurityService.VerifyAsync(appSecurityDto);

            if (!resultVerify.Success)
            {
                if (_environment.IsDevelopment())
                    _loggerFactory.CreateLogger<object>().LogAdvanceError(resultVerify.Message);

                return RpcResultFactory.CreateFromReturnMessage<AppPublicKeyDto>(
                    ReturnMessageFactory.NoData.ToMessage(resultVerify.Message)
                );
            }

            if (resultVerify.Data == null)
            {
                var message = $"appid {appSecurityDto.AppId} do not exist.";

                if (_environment.IsDevelopment())
                    _loggerFactory.CreateLogger<object>().LogAdvanceError(message);

                return RpcResultFactory.CreateFromReturnMessage<AppPublicKeyDto>(
                    ReturnMessageFactory.NoData.ToMessage(message)
                );
            }

            var resultGetAppPublicKey = await _appPublicKeyService.GetAsync();

            return RpcResultFactory.CreateSuccess(
                resultGetAppPublicKey.Data
            );
        }
        catch (Exception e)
        {
            _loggerFactory.CreateLogger<object>().LogAdvanceException(e);

            throw;
        }
    }
}