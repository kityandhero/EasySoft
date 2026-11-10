using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;
using EasySoft.Core.ChannelCheckTransceiver.Consumers;
using EasySoft.Core.ChannelCheckTransceiver.Entities.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Consumers;

/// <summary>
/// ChannelCheckConsumer
/// </summary>
public class ChannelCheckConsumer : BaseChannelCheckConsumer
{
    private readonly ILogger<ChannelCheckConsumer> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IAppSecurityService _appSecurityService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="appSecurityService"></param>
    public ChannelCheckConsumer(
        ILogger<ChannelCheckConsumer> logger,
        IWebHostEnvironment environment,
        IAppSecurityService appSecurityService
    )
    {
        _logger = logger;
        _environment = environment;

        _appSecurityService = appSecurityService;
    }

    /// <inheritdoc />
    public override async Task OnHandle(IChannelCheckMessage message, CancellationToken cancellationToken)
    {
        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(OnHandle)}");

            _logger.LogAdvancePrompt(
                $"Save ChannelCheckMessage -> {message.BuildInfo()}."
            );
        }

        await _appSecurityService.TryCreateAsync(message.Channel);
    }
}