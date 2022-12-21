using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Core.Results.Factories;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Core.Results.Interfaces;

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

                // return new List<AppPublicKeyDto> { new() };
                return RpcResultFactory.CreateFromReturnMessage<AppPublicKeyDto>(
                    ReturnMessage.NoData.ToMessage()
                );

                return new RpcResult<AppPublicKeyDto>()
                {
                    Code = ReturnCode.NoData.ToInt(),
                    Success = false,
                    Message = resultVerify.Message,
                    Data = new AppPublicKeyDto()
                };
            }

            if (resultVerify.Data == null)
            {
                var message = $"appid {appSecurityDto.AppId} do not exist.";

                if (_environment.IsDevelopment())
                    _loggerFactory.CreateLogger<object>().LogAdvanceError(message);

                // return new List<AppPublicKeyDto> { new() };

                return RpcResultFactory.CreateFromReturnMessage<AppPublicKeyDto>(
                    ReturnMessage.NoData
                );

                // return new ApiResult<AppPublicKeyDto>(ReturnCode.NoData)
                // {
                //     Message = message,
                //     Data = new AppPublicKeyDto()
                // };
            }

            var resultGetAppPublicKey = await _appPublicKeyService.GetAsync();

            // if (!resultGetAppPublicKey.Success || resultGetAppPublicKey.Data == null)
            //     return new List<AppPublicKeyDto> { new() };

            // return new List<AppPublicKeyDto> { resultGetAppPublicKey.Data };

            return RpcResultFactory.CreateSuccess(
                resultGetAppPublicKey.Data
            );

            // return new ApiResult<AppPublicKeyDto>(ReturnCode.Ok)
            // {
            //     Data = resultGetAppPublicKey.Data
            // };
        }
        catch (Exception e)
        {
            _loggerFactory.CreateLogger<object>().LogAdvanceException(e);

            throw;
        }
    }
}