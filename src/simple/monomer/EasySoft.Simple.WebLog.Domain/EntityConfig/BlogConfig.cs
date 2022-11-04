using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.WebLog.Domain.Aggregates.BlogAggregate;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.WebLog.Domain.EntityConfig;

public class BlogConfig : BaseEntityTypeConfiguration<Blog>
{
    protected override void ConfigureColumn(EntityTypeBuilder<Blog> builder, Type entityType)
    {
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty)
            .HasComment("博客名称");

        builder.Property(x => x.Pseudonym)
            .HasColumnName("pseudonym")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty)
            .HasComment("");

        builder.Property(x => x.Motto)
            .HasColumnName("motto")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty)
            .HasComment("");

        builder.Property(x => x.CustomerId)
            .HasColumnName("customer_id")
            .HasDefaultValue(0)
            .HasComment("");

        builder.HasMany(x => x.Posts).WithOne().HasForeignKey(y => y.BlogId);
    }
}