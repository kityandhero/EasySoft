using EasySoft.Core.EntityFramework.ValueGenerators;
using EasySoft.Core.Infrastructure.Entities.Implements;
using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Core.EntityFramework.EntityTypeConfigures;

/// <summary>
/// BaseEntityTypeConfiguration
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>,
    IAdvanceEntityTypeConfiguration
    where TEntity : Entity
{
    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="builder"></param>
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

    /// <summary>
    /// ConfigureTable
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected virtual void ConfigureTable(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        var tableName = BuildTableName(entityType);

        if (!string.IsNullOrWhiteSpace(tableName)) builder.ToTable(tableName);

        var tableComment = BuildTableComment(entityType);

        if (!string.IsNullOrWhiteSpace(tableName)) builder.HasComment(tableComment);
    }

    /// <summary>
    /// BuildTableName
    /// </summary>
    /// <param name="entityType"></param>
    /// <returns></returns>
    protected virtual string BuildTableName(Type entityType)
    {
        return "";
    }

    /// <summary>
    /// BuildTableComment
    /// </summary>
    /// <param name="entityType"></param>
    /// <returns></returns>
    protected virtual string BuildTableComment(Type entityType)
    {
        return "";
    }

    /// <summary>
    /// ConfigureKey
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected virtual void ConfigureKey(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnOrder(1)
            .HasColumnName(DatabaseConstant.KeyName)
            .ValueGeneratedNever()
            .HasValueGenerator<IdentifierGenerator>();
    }

    /// <summary>
    /// ConfigureColumn
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected virtual void ConfigureColumn(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
    }

    /// <summary>
    /// ConfigureIndex
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected virtual void ConfigureIndex(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
    }

    /// <summary>
    /// ConfigureConcurrency
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected virtual void ConfigureConcurrency(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        if (typeof(IConcurrency).IsAssignableFrom(entityType))
            builder.Property("RowVersion")
                .HasColumnName("row_version")
                .IsRequired()
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate();
    }

    /// <summary>
    /// ConfigureTenant
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected virtual void ConfigureTenant(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        if (typeof(ITenant).IsAssignableFrom(entityType))
            builder.Property(o => ((ITenant)o).TenantId)
                .HasColumnName("row_version")
                .IsRequired()
                .HasDefaultValue(0);
    }

    /// <summary>
    /// ConfigureQueryFilter
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected virtual void ConfigureQueryFilter(EntityTypeBuilder<TEntity> builder, Type entityType)
    {
        if (!typeof(ISoftDelete).IsAssignableFrom(entityType)) return;

        builder.Property(o => ((ISoftDelete)o).Deleted)
            .HasColumnName("deleted")
            .HasDefaultValue(0)
            .HasColumnOrder(2);

        builder.HasQueryFilter(d => ((ISoftDelete)d).Deleted != Whether.No.ToInt());
    }
}