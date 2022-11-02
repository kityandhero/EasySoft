using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.WebLog.Domain.EntityConfig;

public class BlogConfig : BaseEntityTypeConfiguration<Blog>
{
    public override void Configure(EntityTypeBuilder<Blog> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title).HasMaxLength(200);

        builder.Property(x => x.Author).HasColumnName("author_id");

        builder.OwnsOne(
            x => x.Author,
            y => { y.Property(z => z.Id); }
        );

        builder.OwnsOne(
            x => x.Status,
            y =>
            {
                y.Property(z => z.Code).HasColumnName("status");
                y.Property(z => z.ChangesReason).HasColumnName("status_change_reason").HasMaxLength(200);
            }
        );

        builder.HasMany(x => x.Posts).WithOne().HasForeignKey(y => y.BlogId);
    }
}