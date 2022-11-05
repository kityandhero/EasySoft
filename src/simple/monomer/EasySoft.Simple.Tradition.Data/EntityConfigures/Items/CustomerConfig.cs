using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.Tradition.Data.EntityConfigures.Items;

public class CustomerConfig : BaseEntityTypeConfiguration<Customer>
{
    protected override void ConfigureColumn(EntityTypeBuilder<Customer> builder, Type entityType)
    {
        builder.Property(x => x.UserId)
            .HasDefaultValue(0);
    }
}