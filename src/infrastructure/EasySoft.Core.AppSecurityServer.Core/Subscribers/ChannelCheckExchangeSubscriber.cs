using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;
using EasySoft.Core.ChannelCheckTransmitter.Entities.implements;

namespace EasySoft.Core.AppSecurityServer.Core.Subscribers;

/// <summary>
/// ChannelCheckExchangeSubscriber
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
    [CapSubscribe(TransmitterTopic.ChannelCheckMessage)]
    public async Task Process(ChannelCheckExchange exchange)
    {
        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

            _logger.LogAdvancePrompt(
                $"Save ChannelCheckMessage -> {exchange.BuildInfo()}."
            );
        }

        await _appSecurityService.TryCreateAsync(exchange.Channel);
    }
}