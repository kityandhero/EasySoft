using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.Tradition.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.Tradition.Data.EntityConfig;

public class PostConfig : BaseEntityTypeConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.BlogId).IsRequired();

        builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).HasDefaultValue(string.Empty);

        // builder.Property(x => x.Blog).HasColumnName("blog_id").HasDefaultValue(0);

        // builder.OwnsOne(
        //     x => x.BlogId,
        //     y => { y.Property(z => z.Id).HasColumnName("blog_id").HasDefaultValue(0); }
        // );
    }
}