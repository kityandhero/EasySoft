using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.Tradition.Data.EntityConfigures.Items;

public class UserConfig : BaseEntityTypeConfiguration<User>
{
    protected override void ConfigureColumn(EntityTypeBuilder<User> builder, Type entityType)
    {
        builder.Property(x => x.Alias)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.RealName)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.LoginName)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Password)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(100)
            .HasDefaultValue(string.Empty);

        builder.HasOne(x => x.RoleGroup)
            .WithMany(c => c.Users)
            .HasForeignKey(x => x.RoleGroupId);
    }
}