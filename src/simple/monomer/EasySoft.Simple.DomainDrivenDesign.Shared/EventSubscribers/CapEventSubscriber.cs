using DotNetCore.CAP;
using EasySoft.Core.EventBus.ExtensionMethods;
using EasySoft.Core.EventBus.Trackers;
using EasySoft.Simple.DomainDrivenDesign.Shared.Events;
using Microsoft.Extensions.Logging;

namespace EasySoft.Simple.DomainDrivenDesign.Shared.EventSubscribers;

/// <summary>
/// CapEventSubscriber
/// </summary>
public class CapEventSubscriber : ICapSubscribe
{
    private readonly ILogger<CapEventSubscriber> _logger;
    private readonly IMessageTracker _tracker;

    /// <summary>
    /// CapEventSubscriber
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="trackerFactory"></param>
    public CapEventSubscriber(
        ILogger<CapEventSubscriber> logger,
        MessageTrackerFactory trackerFactory
    )
    {
        _logger = logger;
        _tracker = trackerFactory.Create();
    }

    /// <summary>
    /// 订阅订单创建事件
    /// </summary>
    /// <param name="eventDto"></param>
    /// <returns></returns>
    [CapSubscribe(nameof(AuthorUpdateEvent))]
    public async Task ProcessAuthorUpdateEvent(AuthorUpdateEvent eventDto)
    {
        eventDto.EventTarget = nameof(ProcessAuthorUpdateEvent);

        var hasProcessed = await _tracker.HasProcessedAsync(eventDto);

        if (!hasProcessed)
            await Task.CompletedTask;
    }
}