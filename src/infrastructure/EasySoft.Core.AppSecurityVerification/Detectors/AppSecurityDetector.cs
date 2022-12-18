using EasySoft.Core.AppSecurityVerification.Clients;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.AppSecurityVerification.Detectors;

/// <inheritdoc />
public class AppSecurityDetector : IAppSecurityDetector
{
    private readonly ILoggerFactory _loggerFactory;

    private readonly IApplicationChannel _applicationChannel;

    private readonly IAppSecurityClient _appSecurityClient;

    /// <summary>
    /// 访问探测器
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="applicationChannel"></param>
    /// <param name="appSecurityClient"></param>
    public AppSecurityDetector(
        ILoggerFactory loggerFactory,
        IApplicationChannel applicationChannel,
        IAppSecurityClient appSecurityClient
    )
    {
        _loggerFactory = loggerFactory;
        _applicationChannel = applicationChannel;
        _appSecurityClient = appSecurityClient;
    }

    /// <inheritdoc />
    public async Task Verify()
    {
        var apiResponse = await _appSecurityClient.VerifyAsync(
            new AppSecurityDto
            {
                AppId = GeneralConfigAssist.GetAppId(),
                AppSecret = GeneralConfigAssist.GetAppSecret(),
                Channel = _applicationChannel.GetChannel()
            }
        );

        var logger = _loggerFactory.CreateLogger<AppSecurityDetector>();

        if (!apiResponse.IsSuccessStatusCode)
        {
            var message = $"rpc {GetType().Name}.{nameof(Verify)} call fail";

            logger.LogAdvanceError(message);

            throw new UnknownException(message);
        }

        var list = apiResponse.Content ?? new List<AppPublicKeyDto>();

        if (!list.Any())
        {
            var message = $"rpc {GetType().Name}.{nameof(Verify)} get none AppPublicKey.";

            logger.LogAdvanceError(message);

            throw new UnknownException(message);
        }

        AppSecurityClientConfigure.SetPublicKey(list.First().Key);
    }
}