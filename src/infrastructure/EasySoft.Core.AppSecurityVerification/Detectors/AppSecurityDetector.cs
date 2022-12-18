using EasySoft.Core.AppSecurityVerification.Clients;

namespace EasySoft.Core.AppSecurityVerification.Detectors;

/// <inheritdoc />
public class AppSecurityDetector : IAppSecurityDetector
{
    private readonly ILoggerFactory _loggerFactory;

    private readonly IWebHostEnvironment _environment;

    private readonly IApplicationChannel _applicationChannel;

    private readonly IAppSecurityClient _appSecurityClient;

    /// <summary>
    /// 访问探测器
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    /// <param name="applicationChannel"></param>
    /// <param name="appSecurityClient"></param>
    public AppSecurityDetector(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IApplicationChannel applicationChannel,
        IAppSecurityClient appSecurityClient
    )
    {
        _loggerFactory = loggerFactory;
        _environment = environment;
        _applicationChannel = applicationChannel;
        _appSecurityClient = appSecurityClient;
    }

    /// <inheritdoc />
    public async Task Verify()
    {
        var appSecurityDto = new AppSecurityDto
        {
            AppId = AppSecurityServerConfigure.EmbedMode
                ? AppSecurityAssist.EmbedAppId
                : GeneralConfigAssist.GetAppId(),
            AppSecret = AppSecurityServerConfigure.EmbedMode
                ? AppSecurityAssist.EmbedAppSecret
                : GeneralConfigAssist.GetAppSecret(),
            UnixTime = AppSecurityAssist.GetUnixTime(),
            Salt = AppSecurityAssist.GetSalt(),
            Channel = _applicationChannel.GetChannel()
        };

        appSecurityDto = AppSecurityAssist.Sign(appSecurityDto);

        var logger = _loggerFactory.CreateLogger<AppSecurityDetector>();

        if (_environment.IsDevelopment())
            logger.LogAdvancePrompt(
                $"Will verify AppSecurityDto -> {appSecurityDto.BuildInfo()}"
            );

        var apiResponse = await _appSecurityClient.VerifyAsync(appSecurityDto);

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