using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.Tradition.Data.EntityConfigures.Items;

public class BlogConfig : BaseEntityTypeConfiguration<Blog>
{
    protected override void ConfigureColumn(EntityTypeBuilder<Blog> builder, Type entityType)
    {
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Pseudonym)
            .HasColumnName("pseudonym")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Motto)
            .HasColumnName("motto")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.UserId)
            // .HasColumnName("customer_id")
            .HasDefaultValue(0);

        builder.HasMany(x => x.Posts).WithOne().HasForeignKey(y => y.BlogId);
    }
}