using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;
using EasySoft.Core.ChannelCheckTransmitter.Entities.implements;
using EasySoft.UtilityTools.Standard.Entities.Implements;

namespace EasySoft.Core.AppSecurityServer.Core.Subscribers;

/// <summary>
/// SqlExecutionRecordExchangeSubscriber
/// </summary>
public class ChannelCheckExchangeSubscriber : ICapSubscribe
{
    private readonly ILogger<ChannelCheckExchangeSubscriber> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IAppSecurityService _appSecurityService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="appSecurityService"></param>
    public ChannelCheckExchangeSubscriber(
        ILogger<ChannelCheckExchangeSubscriber> logger,
        IWebHostEnvironment environment,
        IAppSecurityService appSecurityService
    )
    {
        _logger = logger;
        _environment = environment;

        _appSecurityService = appSecurityService;
    }

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="exchange"></param>
    /// <returns></returns>
    [CapSubscribe(TransmitterTopic.ChannelCheckExchange)]
    public async Task Process(ChannelCheckExchange exchange)
    {
        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

            _logger.LogAdvancePrompt(
                $"Save SqlExecutionRecordExchange -> {exchange.BuildInfo()}."
            );
        }

        await _appSecurityService.TryCreateAsync(exchange.Channel);
    }
}