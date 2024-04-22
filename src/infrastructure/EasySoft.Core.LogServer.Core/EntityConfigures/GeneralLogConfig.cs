using EasySoft.Core.LogServer.Core.Entities;

namespace EasySoft.Core.LogServer.Core.EntityConfigures;

/// <summary>
/// GeneralLogConfig
/// </summary>
public class GeneralLogConfig : BaseEntityTypeConfiguration<GeneralLog>
{
    /// <inheritdoc />
    protected override void ConfigureColumn(EntityTypeBuilder<GeneralLog> builder, Type entityType)
    {
        builder.Property(x => x.Message)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(400)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.MessageType)
            .HasDefaultValue(0);

        builder.Property(x => x.Content)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.ContentType)
            .HasDefaultValue(0);

        builder.Property(x => x.Type)
            .HasDefaultValue(0);

        builder.Property(x => x.Channel)
            .HasDefaultValue(0);

        builder.Property(x => x.Status)
            .HasDefaultValue(0);

        builder.Property(x => x.CreateBy)
            .HasDefaultValue(0L);

        builder.Property(x => x.CreateTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);

        builder.Property(x => x.ModifyBy)
            .HasDefaultValue(0L);

        builder.Property(x => x.ModifyTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);
    }
}