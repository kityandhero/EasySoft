using EasySoft.Core.AppSecurityServer.Core.Entities;

namespace EasySoft.Core.AppSecurityServer.Core.EntityConfigures;

/// <summary>
/// AppSecurityConfig
/// </summary>
public class AppSecurityConfig : BaseEntityTypeConfiguration<AppSecurity>
{
    /// <inheritdoc />
    protected override void ConfigureColumn(EntityTypeBuilder<AppSecurity> builder, Type entityType)
    {
        builder.Property(x => x.AppId)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(40)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.AppSecret)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(40)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.SuperRoleRecentlyMaintainTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);

        builder.Property(x => x.SuperRoleNextMaintainTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);

        builder.Property(x => x.MasterControl)
            .HasDefaultValue(0);

        builder.Property(x => x.Channel)
            .HasDefaultValue(0);

        builder.Property(x => x.Deleted)
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