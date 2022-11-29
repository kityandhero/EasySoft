using EasySoft.Simple.Tradition.Service.Events;

namespace EasySoft.Simple.Tradition.Client.WebApi.EventSubscribers;

/// <summary>
/// CapEventSubscriber
/// </summary>
public sealed class CapEventSubscriber : ICapSubscribe
{
    private readonly IUserService _userService;
    private readonly ILogger<CapEventSubscriber> _logger;
    private readonly IMessageTracker _tracker;

    public CapEventSubscriber(
        IUserService userService,
        ILogger<CapEventSubscriber> logger,
        MessageTrackerFactory trackerFactory
    )
    {
        _userService = userService;
        _logger = logger;
        _tracker = trackerFactory.Create();
    }

    /// <summary>
    /// 订阅充值事件
    /// </summary>
    /// <param name="eventDto"></param>
    /// <returns></returns>
    [CapSubscribe(nameof(RegisterSuccessEvent))]
    public async Task ProcessRegisterSuccessEvent(RegisterSuccessEvent eventDto)
    {
        eventDto.EventTarget = nameof(ProcessRegisterSuccessEvent);

        var hasProcessed = await _tracker.HasProcessedAsync(eventDto);

        if (!hasProcessed)
            await _userService.ProcessRegisterSuccessAsync(eventDto, _tracker);
    }
}