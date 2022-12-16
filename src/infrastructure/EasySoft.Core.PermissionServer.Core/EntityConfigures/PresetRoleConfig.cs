using EasySoft.Core.PermissionServer.Core.Entities;

namespace EasySoft.Core.PermissionServer.Core.EntityConfigures;

/// <summary>
/// PresetRoleConfig
/// </summary>
public class PresetRoleConfig : BaseEntityTypeConfiguration<PresetRole>
{
    /// <inheritdoc />
    protected override void ConfigureColumn(EntityTypeBuilder<PresetRole> builder, Type entityType)
    {
        builder.Property(x => x.Name)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(400)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Description)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(500)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Content)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.ModuleCount)
            .HasDefaultValue(0);

        builder.Property(x => x.Competence)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.WhetherSuper)
            .HasDefaultValue(0);

        builder.Property(x => x.Status)
            .HasDefaultValue(0);

        builder.Property(x => x.Ip)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(40)
            .HasDefaultValue(string.Empty);

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