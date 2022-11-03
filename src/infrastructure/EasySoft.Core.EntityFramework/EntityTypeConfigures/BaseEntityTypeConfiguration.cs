using EasySoft.Core.EntityFramework.ValueGenerators;
using EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;
using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;
using EasySoft.UtilityTools.Standard;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasySoft.Core.EntityFramework.EntityTypeConfigures;

public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>,
    IAdvanceEntityTypeConfiguration
    where TEntity : Entity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        var entityType = typeof(TEntity);

        ConfigureTableName(builder, entityType);
        ConfigureKey(builder, entityType);
        ConfigureColumn(builder, entityType);
        ConfigureConcurrency(builder, entityType);
        ConfigureQueryFilter(builder, entityType);
    }

    protected virtual void ConfigureTableName(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        builder.ToTable(entityType.Name.ToSnakeCase());
    }

    protected virtual void ConfigureKey(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnOrder(1)
            .HasColumnName(DatabaseConstant.KeyName)
            .ValueGeneratedNever()
            .HasValueGenerator<IdentifierGenerator>()
            .HasDefaultValue(0)
            .HasComment("主键标识");
    }

    protected virtual void ConfigureColumn(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
    }

    protected virtual void ConfigureConcurrency(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        if (typeof(IConcurrency).IsAssignableFrom(entityType))
            builder.Property("RowVersion").IsRequired().IsRowVersion().ValueGeneratedOnAddOrUpdate();
    }

    protected virtual void ConfigureQueryFilter(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        if (!typeof(ISoftDelete).IsAssignableFrom(entityType)) return;

        builder.Property("IsDeleted")
            .HasDefaultValue(false)
            .HasColumnOrder(2);

        builder.HasQueryFilter(d => !EF.Property<bool>(d, "IsDeleted"));
    }
}