using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Subscribers;

/// <summary>
/// SqlLogExchangeSubscriber
/// </summary>
public class SqlLogExchangeSubscriber : ICapSubscribe
{
    private readonly ILogger<SqlLogExchangeSubscriber> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly ISqlLogService _sqlLogService;

    /// <summary>
    /// GeneralLogExchangeSubscriber
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="sqlLogService"></param>
    public SqlLogExchangeSubscriber(
        ILogger<SqlLogExchangeSubscriber> logger,
        IWebHostEnvironment environment,
        ISqlLogService sqlLogService
    )
    {
        _logger = logger;
        _environment = environment;

        _sqlLogService = sqlLogService;
    }

    /// <summary>
    /// 订阅充值事件
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [CapSubscribe(TransmitterTopic.SqlLogMessage)]
    public async Task Process(ISqlLogMessage message)
    {
        if (message.Ignore > 0)
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
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

            _logger.LogAdvancePrompt(
                $"Save SqlLogExchange -> {message.BuildInfo()}."
            );
        }

        await _sqlLogService.SaveAsync(message);
    }
}