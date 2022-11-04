using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.WebLog.Domain.Aggregates.BlogAggregate;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.WebLog.Domain.EntityConfig;

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
            .HasComment("帖子标题");
    }
}