using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Subscribers;

/// <summary>
/// ErrorLogExchangeSubscriber
/// </summary>
public sealed class ErrorLogExchangeSubscriber : ICapSubscribe
{
    private readonly ILogger<ErrorLogExchangeSubscriber> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IErrorLogService _errorLogService;

    /// <summary>
    /// ErrorLogExchangeSubscriber
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="errorLogService"></param>
    public ErrorLogExchangeSubscriber(
        ILogger<ErrorLogExchangeSubscriber> logger,
        IWebHostEnvironment environment,
        IErrorLogService errorLogService
    )
    {
        _logger = logger;
        _environment = environment;

        _errorLogService = errorLogService;
    }

    /// <summary>
    /// 订阅充值事件
    /// </summary>
    /// <param name="errorLogExchange"></param>
    /// <returns></returns>
    [CapSubscribe(TransmitterTopic.ErrorLogExchange)]
    public async Task Process(ErrorLogExchange errorLogExchange)
    {
        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

            _logger.LogAdvancePrompt(
                $"Save ErrorLogExchange -> {errorLogExchange.BuildInfo()}."
            );
        }

        await _errorLogService.SaveAsync(errorLogExchange);
    }
}