using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Subscribers;

/// <summary>
/// GeneralLogExchangeSubscriber
/// </summary>
public sealed class GeneralLogExchangeSubscriber : ICapSubscribe
{
    private readonly ILogger<GeneralLogExchangeSubscriber> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IGeneralLogService _generalLogService;

    /// <summary>
    /// GeneralLogExchangeSubscriber
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="generalLogService"></param>
    public GeneralLogExchangeSubscriber(
        ILogger<GeneralLogExchangeSubscriber> logger,
        IWebHostEnvironment environment,
        IGeneralLogService generalLogService
    )
    {
        _logger = logger;
        _environment = environment;

        _generalLogService = generalLogService;
    }

    /// <summary>
    /// 订阅充值事件
    /// </summary>
    /// <param name="generalLogMessage"></param>
    /// <returns></returns>
    [CapSubscribe(TransmitterTopic.GeneralLogMessage)]
    public async Task Process(IGeneralLogMessage generalLogMessage)
    {
        if (generalLogMessage.Ignore > 0)
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
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

            _logger.LogAdvancePrompt(
                $"Save GeneralLogMessage -> {generalLogMessage.BuildInfo()}."
            );
        }

        await _generalLogService.SaveAsync(generalLogMessage);
    }
}