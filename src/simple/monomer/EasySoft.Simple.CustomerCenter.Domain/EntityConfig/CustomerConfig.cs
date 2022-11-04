using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.Simple.CustomerCenter.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Simple.CustomerCenter.Domain.EntityConfig;

public class CustomerConfig : BaseEntityTypeConfiguration<Customer>
{
    protected override void ConfigureColumn(EntityTypeBuilder<Customer> builder, Type entityType)
    {
    }
}