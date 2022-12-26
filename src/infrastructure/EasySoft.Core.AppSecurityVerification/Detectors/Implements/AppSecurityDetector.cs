using EasySoft.Core.AppSecurityVerification.Clients;
using EasySoft.Core.AppSecurityVerification.Detectors.Interfaces;
using EasySoft.Core.ChannelCheckTransmitter.Producers;

namespace EasySoft.Core.AppSecurityVerification.Detectors.Implements;

/// <inheritdoc />
public class AppSecurityDetector : IAppSecurityDetector
{
    private readonly ILoggerFactory _loggerFactory;

    private readonly IWebHostEnvironment _environment;

    private readonly IMediator _mediator;

    private readonly IApplicationChannel _applicationChannel;

    private readonly IAppSecurityClient _appSecurityClient;
    private readonly IChannelCheckProducer _channelCheckProducer;

    /// <summary>
    /// 访问探测器
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    /// <param name="mediator"></param>
    /// <param name="applicationChannel"></param>
    /// <param name="appSecurityClient"></param>
    /// <param name="channelCheckProducer"></param>
    public AppSecurityDetector(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IMediator mediator,
        IApplicationChannel applicationChannel,
        IAppSecurityClient appSecurityClient,
        IChannelCheckProducer channelCheckProducer
    )
    {
        _loggerFactory = loggerFactory;
        _environment = environment;
        _mediator = mediator;
        _applicationChannel = applicationChannel;
        _appSecurityClient = appSecurityClient;
        _channelCheckProducer = channelCheckProducer;
    }

    /// <inheritdoc />
    public async Task ChannelCheck()
    {
        await _channelCheckProducer.SendAsync();
    }

    /// <inheritdoc />
    public async Task CredentialVerify()
    {
        var appSecurityDto = new AppSecurityDto
        {
            AppId = GeneralConfigAssist.GetAppId(),
            AppSecret = GeneralConfigAssist.GetAppSecret(),
            UnixTime = AppSecurityAssist.GetUnixTime(),
            Salt = AppSecurityAssist.GetSalt(),
            Channel = _applicationChannel.GetChannel()
        };

        appSecurityDto = AppSecurityAssist.SignVerify(appSecurityDto);

        var logger = _loggerFactory.CreateLogger<AppSecurityDetector>();

        if (_environment.IsDevelopment())
            logger.LogAdvancePrompt(
                $"Will verify AppSecurityDto -> {appSecurityDto.BuildInfo()}"
            );

        var apiResponse = await _appSecurityClient.CredentialVerifyAsync(appSecurityDto);

        if (!apiResponse.IsSuccessStatusCode || apiResponse.Content == null)
        {
            var message = $"rpc {GetType().Name}.{nameof(CredentialVerify)} call fail";

            logger.LogAdvanceError(message);

            throw new UnknownException(message);
        }

        var result = apiResponse.Content;

        if (!result.Success || result.Data == null)
        {
            var message = $"rpc get none AppPublicKey -> {result.Message}.";

            logger.LogAdvanceError(message);

            throw new UnknownException(message);
        }

        AppSecurityClientConfigure.SetPublicKey(result.Data.Key);

        var appSecurityFirstVerifyNotification = new AppSecurityFirstVerifyNotification(true);

        if (_environment.IsDevelopment())
            logger.LogAdvancePrompt(
                $"Send mediator Notification {nameof(AppSecurityFirstVerifyNotification)} -> {appSecurityFirstVerifyNotification.BuildInfo()}"
            );

        await _mediator.Publish(appSecurityFirstVerifyNotification);
    }
}