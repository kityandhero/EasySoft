using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Core.EntityFramework.ValueGenerators;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.Tradition.Data.EntityConfig;

public class PostConfig : BaseEntityTypeConfiguration<Post>
{
    protected override void ConfigureColumn(EntityTypeBuilder<Post> builder, Type entityType)
    {
        builder.Property(x => x.BlogId).IsRequired();

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty)
            .HasComment("");

        // builder.Property(x => x.Blog).HasColumnName("blog_id").HasDefaultValue(0);

        // builder.OwnsOne(
        //     x => x.BlogId,
        //     y => { y.Property(z => z.Id).HasColumnName("blog_id").HasDefaultValue(0); }
        // );
    }
}