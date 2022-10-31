using EasySoft.Core.EventBus.Events;
using EasySoft.Core.EventBus.Trackers;

namespace EasySoft.Core.EventBus.ExtensionMethods;

public static class MessageTrackerExtension
{
    public static async Task<bool> HasProcessedAsync<T>(
        this IMessageTracker tracker,
        EventEntity<T> eventDto
    ) where T : class
    {
        return await tracker.HasProcessedAsync(eventDto.Id, eventDto.EventTarget);
    }

    public static async Task MarkAsProcessedAsync<T>(
        this IMessageTracker tracker,
        EventEntity<T> eventDto
    ) where T : class
    {
        await tracker.MarkAsProcessedAsync(eventDto.Id, eventDto.EventTarget);
    }
}