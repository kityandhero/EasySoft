using EasySoft.Core.PermissionServer.Core.Entities;

namespace EasySoft.Core.PermissionServer.Core.EntityConfigures;

/// <summary>
/// RoleGroupConfig
/// </summary>
public class RoleGroupConfig : BaseEntityTypeConfiguration<RoleGroup>
{
    /// <inheritdoc />
    protected override void ConfigureColumn(EntityTypeBuilder<RoleGroup> builder, Type entityType)
    {
        builder.Property(x => x.Name)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.CustomRoleCollection)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.PresetRoleCollection)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

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