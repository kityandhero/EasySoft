namespace EasySoft.Core.EventBus.Trackers;

public sealed class MessageTrackerFactory
{
    public readonly IEnumerable<IMessageTracker> Trackers;

    public MessageTrackerFactory(IEnumerable<IMessageTracker> trackers)
    {
        Trackers = trackers;
    }

    public IMessageTracker Create(TrackerKind kind = TrackerKind.Database)
    {
        if (Trackers.IsNullOrEmpty())
            return new NullMessageTrackerService();

        return Trackers.FirstOrDefault(x => x.Kind == kind) ?? new NullMessageTrackerService();
    }
}