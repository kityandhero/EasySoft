using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.Tradition.Data.EntityConfig;

public class CustomerConfig : BaseEntityTypeConfiguration<Customer>
{
    protected override void ConfigureColumn(EntityTypeBuilder<Customer> builder, Type entityType)
    {
        builder.Property(x => x.Alias)
            .HasColumnName("alias")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty)
            .HasComment("昵称");

        builder.Property(x => x.RealName)
            .HasColumnName("real_name")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty)
            .HasComment("真实姓名");

        builder.Property(x => x.LoginName)
            .HasColumnName("login_name")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty)
            .HasComment("登录名");

        builder.Property(x => x.Password)
            .HasColumnName("password")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(100)
            .HasDefaultValue(string.Empty)
            .HasComment("密码");
    }
}