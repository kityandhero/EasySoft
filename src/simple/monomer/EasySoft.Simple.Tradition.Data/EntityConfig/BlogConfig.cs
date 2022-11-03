using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.Tradition.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.Tradition.Data.EntityConfig;

public class BlogConfig : BaseEntityTypeConfiguration<Blog>
{
    public override void Configure(EntityTypeBuilder<Blog> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).HasDefaultValue(string.Empty);

        builder.Property(x => x.Pseudonym).HasColumnName("pseudonym").HasMaxLength(200).HasDefaultValue(string.Empty);

        builder.Property(x => x.Motto).HasColumnName("motto").HasMaxLength(200).HasDefaultValue(string.Empty);

        builder.Property(x => x.CustomerId).HasColumnName("customer_id").HasDefaultValue(0);

        builder.HasMany(x => x.Posts).WithOne().HasForeignKey(y => y.BlogId);
    }
}