using EasySoft.Core.ErrorLogTransceiver.Consumers;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Consumers;

/// <summary>
/// ErrorLogConsumer
/// </summary>
public sealed class ErrorLogConsumer : BaseErrorLogConsumer
{
    private readonly ILogger<ErrorLogConsumer> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IErrorLogService _errorLogService;

    /// <summary>
    /// ErrorLogConsumer
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="errorLogService"></param>
    public ErrorLogConsumer(
        ILogger<ErrorLogConsumer> logger,
        IWebHostEnvironment environment,
        IErrorLogService errorLogService
    )
    {
        _logger = logger;
        _environment = environment;

        _errorLogService = errorLogService;
    }

    /// <inheritdoc />
    public override async Task OnHandle(
        IErrorLogTransitiveMessage transitiveMessage,
        CancellationToken cancellationToken
    )
    {
        if (transitiveMessage.Ignore > 0)
        {
            if (_environment.IsDevelopment())
            {
                _logger.LogAdvancePrompt(
                    "ErrorLog ignore process."
                );
            }

            return;
        }

        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(OnHandle)}");

            _logger.LogAdvancePrompt(
                $"Save IErrorLogTransitiveMessage -> {transitiveMessage.BuildInfo()}."
            );
        }

        await _errorLogService.SaveAsync(transitiveMessage);
    }
}