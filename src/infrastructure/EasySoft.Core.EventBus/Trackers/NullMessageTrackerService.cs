namespace EasySoft.Core.EventBus.Trackers;

public class NullMessageTrackerService : IMessageTracker
{
    public TrackerKind Kind => TrackerKind.Null;

    public async Task<bool> HasProcessedAsync(long eventId, string trackeName)
    {
        return await Task.FromResult(false);
    }

    public async Task MarkAsProcessedAsync(long eventId, string trackeName)
    {
        await Task.CompletedTask;
    }
}