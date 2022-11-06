using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.AccountCenter.Domain.Aggregates.AccountAggregate;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.AccountCenter.Domain.EntityConfigures.Items;

public class UserConfig : BaseEntityTypeConfiguration<User>
{
    protected override void ConfigureColumn(EntityTypeBuilder<User> builder, Type entityType)
    {
        builder.Property(x => x.Alias)
            .HasColumnName("alias")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.RealName)
            .HasColumnName("real_name")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.LoginName)
            .HasColumnName("login_name")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Password)
            .HasColumnName("password")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(100)
            .HasDefaultValue(string.Empty);
    }
}