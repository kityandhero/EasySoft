namespace EasySoft.Core.EventBus.Trackers;

public interface IMessageTracker
{
    TrackerKind Kind { get; }

    Task<bool> HasProcessedAsync(long eventId, string trackerName);

    Task MarkAsProcessedAsync(long eventId, string trackerName);
}