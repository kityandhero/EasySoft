using EasySoft.Core.AccessWayTransceiver.Consumers;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Consumers;

/// <summary>
/// CapEventSubscriber
/// </summary>
public sealed class AccessWayExchangeSubscriber : BaseAccessWayConsumer
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

    /// <inheritdoc />
    public override async Task OnHandle(IAccessWayMessage message, CancellationToken cancellationToken)
    {
        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(OnHandle)}");

            _logger.LogAdvancePrompt(
                $"Save AccessWayMessage -> {message.BuildInfo()}."
            );
        }

        await _accessWayService.SaveAccessWayAsync(message);
    }
}