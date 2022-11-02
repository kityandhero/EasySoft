using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.WebLog.Domain.EntityConfig;

public class AuthorConfig : BaseEntityTypeConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.CustomerId).HasColumnName("customer_id");

        builder.Property(x => x.Motto).HasColumnName("motto").HasMaxLength(200);

        builder.Property(x => x.Pseudonym).HasColumnName("pseudonym").HasMaxLength(200);

        builder.Property(x => x.Blog).HasColumnName("blog_id");

        builder.OwnsOne(
            x => x.Blog,
            y => { y.Property(z => z.Id); }
        );
    }
}