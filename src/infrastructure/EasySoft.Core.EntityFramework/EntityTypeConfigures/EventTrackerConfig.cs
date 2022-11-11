namespace EasySoft.Core.EntityFramework.EntityTypeConfigures;

public class EventTrackerConfig : BaseEntityTypeConfiguration<EventTracker>
{
    protected override void ConfigureColumn(EntityTypeBuilder<EventTracker> builder, Type entityType)
    {
        builder.Property(x => x.TrackerName).HasMaxLength(50);

        builder.HasIndex(x => new { x.EventId, x.TrackerName }, "uk_event_id_tracker_name").IsUnique();
    }
}