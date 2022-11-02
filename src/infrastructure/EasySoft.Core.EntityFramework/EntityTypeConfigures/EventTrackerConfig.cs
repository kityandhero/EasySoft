using EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Core.EntityFramework.EntityTypeConfigures;

public class EventTrackerConfig : BaseEntityTypeConfiguration<EventTracker>
{
    public override void Configure(EntityTypeBuilder<EventTracker> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.TrackerName).HasMaxLength(50);
        builder.HasIndex(x => new { x.EventId, x.TrackerName }, "uk_event_id_tracker_name").IsUnique();
    }
}