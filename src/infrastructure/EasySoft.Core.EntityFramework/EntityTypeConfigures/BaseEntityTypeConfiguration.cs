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

        ConfigureTable(builder, entityType);
        ConfigureKey(builder, entityType);
        ConfigureColumn(builder, entityType);
        ConfigureIndex(builder, entityType);
        ConfigureConcurrency(builder, entityType);
        ConfigureQueryFilter(builder, entityType);

        ConfigureTenant(builder, entityType);
    }

    protected virtual void ConfigureTable(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        builder.ToTable(BuildTableName(entityType))
            .HasComment(BuildTableComment(entityType));
    }

    protected virtual string BuildTableName(Type entityType)
    {
        return entityType.Name.ToSnakeCase();
    }

    protected virtual string BuildTableComment(Type entityType)
    {
        return entityType.Name.ToCamelCase();
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

    protected virtual void ConfigureIndex(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
    }

    protected virtual void ConfigureConcurrency(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        if (typeof(IConcurrency).IsAssignableFrom(entityType))
            builder.Property("RowVersion")
                .HasColumnName("row_version")
                .IsRequired()
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate();
    }

    protected virtual void ConfigureTenant(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        if (typeof(ITenant).IsAssignableFrom(entityType))
            builder.Property(o => ((ITenant)o).TenantId)
                .HasColumnName("row_version")
                .IsRequired()
                .HasDefaultValue(0)
                .HasComment("租户标识");
    }

    protected virtual void ConfigureQueryFilter(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        if (!typeof(ISoftDelete).IsAssignableFrom(entityType)) return;

        builder.Property(o => ((ISoftDelete)o).Deleted)
            .HasColumnName("deleted")
            .HasDefaultValue(0)
            .HasColumnOrder(2);

        builder.HasQueryFilter(d => !EF.Property<bool>(d, "Deleted"));
    }
}