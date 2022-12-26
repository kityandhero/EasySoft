using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Controllers;

/// <summary>
/// rpc controller
/// </summary>
[Route("appSecurityRpc")]
public class RpcController : CustomControllerBase
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IAppSecurityRpcService _appSecurityRpcService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="appSecurityRpcService"></param>
    /// <param name="loggerFactory"></param>
    public RpcController(
        IAppSecurityRpcService appSecurityRpcService,
        ILoggerFactory loggerFactory
    )
    {
        _appSecurityRpcService = appSecurityRpcService;
        _loggerFactory = loggerFactory;
    }

    /// <summary>
    /// Verify
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    [Route("credentialVerify")]
    [HttpPost]
    public async Task<RpcResult<AppPublicKeyDto>> CredentialVerify([FromBody] AppSecurityDto appSecurityDto)
    {
        try
        {
            var resultVerify = await _appSecurityRpcService.CredentialVerifyAsync(appSecurityDto);

            if (!resultVerify.Success || resultVerify.Data == null)
                return RpcResultFactory.CreateFromReturnMessage<AppPublicKeyDto>(
                    resultVerify.Code
                );

            return RpcResultFactory.CreateSuccess(
                resultVerify.Data
            );
        }
        catch (Exception e)
        {
            _loggerFactory.CreateLogger<object>().LogAdvanceException(e);

            throw;
        }
    }
}