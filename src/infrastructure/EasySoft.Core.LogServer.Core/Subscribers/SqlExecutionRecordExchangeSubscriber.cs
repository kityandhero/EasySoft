using EasySoft.Core.LogServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Entities.Implements;

namespace EasySoft.Core.LogServer.Core.Subscribers;

/// <summary>
/// SqlExecutionRecordExchangeSubscriber
/// </summary>
public class SqlExecutionRecordExchangeSubscriber : ICapSubscribe
{
    private readonly ILogger<SqlExecutionRecordExchangeSubscriber> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly ISqlExecutionRecordService _sqlExecutionRecordService;

    /// <summary>
    /// GeneralLogExchangeSubscriber
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="sqlExecutionRecordService"></param>
    public SqlExecutionRecordExchangeSubscriber(
        ILogger<SqlExecutionRecordExchangeSubscriber> logger,
        IWebHostEnvironment environment,
        ISqlExecutionRecordService sqlExecutionRecordService
    )
    {
        _logger = logger;
        _environment = environment;

        _sqlExecutionRecordService = sqlExecutionRecordService;
    }

    /// <summary>
    /// 订阅充值事件
    /// </summary>
    /// <param name="exchange"></param>
    /// <returns></returns>
    [CapSubscribe(TransmitterTopic.SqlExecutionRecordExchange)]
    public async Task Process(SqlExecutionRecordExchange exchange)
    {
        if (exchange.Ignore > 0)
        {
            if (_environment.IsDevelopment())
                _logger.LogAdvancePrompt(
                    "SqlExecutionRecord ignore process."
                );

            return;
        }

        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

            _logger.LogAdvancePrompt(
                $"Save SqlExecutionRecordExchange -> {exchange.BuildInfo()}."
            );
        }

        await _sqlExecutionRecordService.SaveAsync(exchange);
    }
}