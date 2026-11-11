using EasySoft.Core.LogServer.Core.Services.Interfaces;
using EasySoft.Core.SqlLogTransceiver.Consumers;

namespace EasySoft.Core.LogServer.Core.Consumers;

/// <summary>
/// SqlLogConsumer
/// </summary>
public class SqlLogConsumer : BaseSqlLogConsumer
{
    private readonly ILogger<SqlLogConsumer> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly ISqlLogService _sqlLogService;

    /// <summary>
    /// GeneralLogConsumer
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="sqlLogService"></param>
    public SqlLogConsumer(
        ILogger<SqlLogConsumer> logger,
        IWebHostEnvironment environment,
        ISqlLogService sqlLogService
    )
    {
        _logger = logger;
        _environment = environment;

        _sqlLogService = sqlLogService;
    }

    /// <inheritdoc />
    public override async Task OnHandle(ISqlLogTransitiveMessage transitiveMessage, CancellationToken cancellationToken)
    {
        if (transitiveMessage.Ignore > 0)
        {
            if (_environment.IsDevelopment())
            {
                _logger.LogAdvancePrompt(
                    "SqlLog ignore process."
                );
            }

            return;
        }

        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(OnHandle)}");

            _logger.LogAdvancePrompt(
                $"Save SqlLogExchange -> {transitiveMessage.BuildInfo()}."
            );
        }

        await _sqlLogService.SaveAsync(transitiveMessage);
    }
}