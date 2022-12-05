using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.Tradition.Data.EntityConfigures.Items;

public class RoleGroupConfig : BaseEntityTypeConfiguration<RoleGroup>
{
    protected override void ConfigureColumn(EntityTypeBuilder<RoleGroup> builder, Type entityType)
    {
        builder.Property(x => x.CustomRoleCollection)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.PresetRoleCollection)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Channel)
            .HasMaxLength(50)
            .HasDefaultValue(0);
    }
}