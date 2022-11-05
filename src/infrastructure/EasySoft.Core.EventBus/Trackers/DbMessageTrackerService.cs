using EasySoft.Core.Data.Repositories;
using EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;
using EasySoft.IdGenerator.Assists;

namespace EasySoft.Core.EventBus.Trackers;

public class DbMessageTrackerService : IMessageTracker
{
    private readonly IRepository<EventTracker> _trackerRepository;

    public DbMessageTrackerService(IRepository<EventTracker> trackerRepository)
    {
        _trackerRepository = trackerRepository;
    }

    public TrackerKind Kind => TrackerKind.Database;

    public async Task<bool> HasProcessedAsync(long eventId, string trackerName)
    {
        var result = await _trackerRepository.ExistAsync(
            x => x.EventId == eventId && x.TrackerName == trackerName,
            true
        );

        return result.Success;
    }

    public async Task MarkAsProcessedAsync(long eventId, string trackerName)
    {
        await _trackerRepository.CreateAsync(new EventTracker
        {
            Id = IdentifierAssist.Create(),
            EventId = eventId,
            TrackerName = trackerName
        });
    }
}