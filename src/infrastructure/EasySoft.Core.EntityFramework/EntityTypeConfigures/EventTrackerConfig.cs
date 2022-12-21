using EasySoft.Core.Infrastructure.Repositories.Entities.Implements;

namespace EasySoft.Core.EntityFramework.EntityTypeConfigures;

/// <summary>
/// EventTrackerConfig
/// </summary>
public class EventTrackerConfig : BaseEntityTypeConfiguration<EventTracker>
{
    /// <summary>
    /// ConfigureColumn
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected override void ConfigureColumn(EntityTypeBuilder<EventTracker> builder, Type entityType)
    {
        builder.Property(x => x.TrackerName).HasMaxLength(50);

        builder.HasIndex(x => new { x.EventId, x.TrackerName }, "uk_event_id_tracker_name").IsUnique();
    }
}