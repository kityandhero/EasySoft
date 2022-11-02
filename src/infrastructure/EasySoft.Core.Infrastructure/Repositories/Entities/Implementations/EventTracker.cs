namespace EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;

public class EventTracker : Entity
{
    public long EventId { get; set; }

    public string TrackerName { get; set; } = string.Empty;
}