using EasySoft.Core.PermissionServer.Services.Interfaces;
using EasySoft.UtilityTools.Core.ExtensionMethods;

namespace EasySoft.Core.PermissionServer.Subscribers;

/// <summary>
/// CapEventSubscriber
/// </summary>
public sealed class AccessWayExchangeSubscriber : ICapSubscribe
{
    private readonly ILogger<AccessWayExchangeSubscriber> _logger;
    private readonly ISecurityService _securityService;

    public AccessWayExchangeSubscriber(
        ILogger<AccessWayExchangeSubscriber> logger,
        ISecurityService securityService
    )
    {
        _securityService = securityService;
        _logger = logger;
    }

    /// <summary>
    /// 订阅充值事件
    /// </summary>
    /// <param name="accessWayExchange"></param>
    /// <returns></returns>
    [CapSubscribe(TransmitterTopic.AccessWayExchange)]
    public async Task Process(AccessWayExchange accessWayExchange)
    {
        _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

        await _securityService.SaveAccessWayModelAsync(accessWayExchange);
    }
}