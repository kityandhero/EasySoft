using EasySoft.Core.LogServer.Core.Entities;

namespace EasySoft.Core.LogServer.Core.EntityConfigures;

/// <summary>
/// SqlExecutionRecordConfig
/// </summary>
public class SqlExecutionRecordConfig : BaseEntityTypeConfiguration<SqlExecutionRecord>
{
    /// <inheritdoc />
    protected override void ConfigureColumn(EntityTypeBuilder<SqlExecutionRecord> builder, Type entityType)
    {
        builder.Property(x => x.CommandString)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.ExecuteType)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(0);

        builder.Property(x => x.ExecuteTypeSource)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.StackTraceSnippet)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.StartMilliseconds)
            .HasColumnType(DatabaseConstant.Decimal_18_6)
            .HasDefaultValue(0);

        builder.Property(x => x.DurationMilliseconds)
            .HasColumnType(DatabaseConstant.Decimal_18_6)
            .HasDefaultValue(0);

        builder.Property(x => x.FirstFetchDurationMilliseconds)
            .HasColumnType(DatabaseConstant.Decimal_18_6)
            .HasDefaultValue(0);

        builder.Property(x => x.Errored)
            .HasDefaultValue(0);

        builder.Property(x => x.CollectMode)
            .HasDefaultValue(0);

        builder.Property(x => x.DatabaseChannel)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(400)
            .HasDefaultValue(string.Empty);

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