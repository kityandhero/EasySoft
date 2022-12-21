using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Subscribers;

/// <summary>
/// CapEventSubscriber
/// </summary>
public sealed class AccessWayExchangeSubscriber : ICapSubscribe
{
    private readonly ILogger<AccessWayExchangeSubscriber> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IAccessWayService _accessWayService;

    /// <summary>
    /// AccessWayExchangeSubscriber
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="accessWayService"></param>
    public AccessWayExchangeSubscriber(
        ILogger<AccessWayExchangeSubscriber> logger,
        IWebHostEnvironment environment,
        IAccessWayService accessWayService
    )
    {
        _logger = logger;
        _environment = environment;

        _accessWayService = accessWayService;
    }

    /// <summary>
    /// 订阅充值事件
    /// </summary>
    /// <param name="accessWayExchange"></param>
    /// <returns></returns>
    [CapSubscribe(TransmitterTopic.AccessWayExchange)]
    public async Task Process(AccessWayExchange accessWayExchange)
    {
        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

            _logger.LogAdvancePrompt(
                $"Save AccessWayExchange -> {accessWayExchange.BuildInfo()}."
            );
        }

        await _accessWayService.SaveAccessWayAsync(accessWayExchange);
    }
}