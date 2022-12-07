using EasySoft.Core.Permission.Server.Entities;

namespace EasySoft.Core.Permission.Server.EntityConfigures.Items;

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