using EasySoft.Core.GeneralLogTransceiver.Consumers;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Consumers;

/// <summary>
/// GeneralLogConsumer
/// </summary>
public sealed class GeneralLogConsumer : BaseGeneralLogConsumer
{
    private readonly ILogger<GeneralLogConsumer> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IGeneralLogService _generalLogService;

    /// <summary>
    /// GeneralLogConsumer
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="generalLogService"></param>
    public GeneralLogConsumer(
        ILogger<GeneralLogConsumer> logger,
        IWebHostEnvironment environment,
        IGeneralLogService generalLogService
    )
    {
        _logger = logger;
        _environment = environment;

        _generalLogService = generalLogService;
    }

    /// <inheritdoc />
    public override async Task OnHandle(
        IGeneralLogTransitiveMessage transitiveMessage,
        CancellationToken cancellationToken
    )
    {
        if (transitiveMessage.Ignore > 0)
        {
            if (_environment.IsDevelopment())
            {
                _logger.LogAdvancePrompt(
                    "GeneralLog ignore process."
                );
            }

            return;
        }

        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(OnHandle)}");

            _logger.LogAdvancePrompt(
                $"Save GeneralLogTransitiveMessage -> {transitiveMessage.BuildInfo()}."
            );
        }

        await _generalLogService.SaveAsync(transitiveMessage);
    }
}