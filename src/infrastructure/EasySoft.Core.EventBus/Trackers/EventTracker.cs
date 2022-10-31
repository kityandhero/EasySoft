using EasySoft.Core.Infrastructure.Entities.implementations;

namespace EasySoft.Core.EventBus.Trackers;

public class EventTracker : Entity
{
    public long EventId { get; set; }

    public string TrackerName { get; set; } = string.Empty;
}