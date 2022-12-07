using EasySoft.Core.PermissionServer.Entities;

namespace EasySoft.Core.PermissionServer.EntityConfigures.Items;

public class RoleGroupConfig : BaseEntityTypeConfiguration<RoleGroup>
{
    protected override void ConfigureColumn(EntityTypeBuilder<RoleGroup> builder, Type entityType)
    {
        builder.Property(x => x.Name)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.CustomRoleCollection)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.PresetRoleCollection)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasDefaultValue(string.Empty);
    }
}