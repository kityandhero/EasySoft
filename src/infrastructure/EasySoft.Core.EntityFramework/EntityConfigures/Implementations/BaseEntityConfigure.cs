using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;

namespace EasySoft.Core.EntityFramework.EntityConfigures.Implementations;

/// <summary>
///  适用于 EntityFramework Core 实体配置
/// </summary>
public abstract class BaseEntityConfigure : IEntityConfigure
{
    protected abstract IEnumerable<Assembly> GetEntityAssemblies();

    /// <summary>
    /// 获取实体类型集合
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    protected virtual IEnumerable<Type> GetEntityTypes(Assembly assembly)
    {
        var typeList = assembly.GetTypes().Where(m =>
            m.FullName != null
            && typeof(Entity).IsAssignableFrom(m)
            && !m.IsAbstract);

        return typeList.Append(typeof(EventTracker));
    }

    public virtual void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entityAssemblies = GetEntityAssemblies();

        var assemblies = new List<Assembly> { typeof(EventTracker).Assembly };

        assemblies.AddRange(entityAssemblies);

        assemblies = assemblies.Distinct().ToList();

        var entityTypes = new List<Type>();

        assemblies.ForEach(assembly =>
        {
            entityTypes.AddRange(GetEntityTypes(assembly).ToList());

            entityTypes.ForEach(t => modelBuilder.Entity(t));
        });

        SetTable(modelBuilder, entityTypes);

        assemblies.ForEach(assembly => modelBuilder.ApplyConfigurationsFromAssembly(assembly));
    }

    #region Static

    private static void SetTable(ModelBuilder modelBuilder, IEnumerable<Type>? types)
    {
        if (types is null)
            return;

        var entityTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(x => types.Contains(x.ClrType));

        entityTypes.ForEach(entityType =>
        {
            modelBuilder.Entity(
                entityType.Name,
                builder =>
                {
                    builder.ToTable(entityType.ClrType.Name.ToSnakeCase());

                    var descriptionTable = entityType.ClrType.GetDescriptionAttributeText();

                    if (!string.IsNullOrWhiteSpace(descriptionTable)) builder.HasComment(descriptionTable);

                    entityType.GetProperties().ForEach(property =>
                    {
                        var propertyName = property.Name;
                        var memberInfo = entityType.ClrType
                            .GetMember(propertyName)
                            .FirstOrDefault();

                        if (memberInfo == null) return;

                        var descriptionMember = memberInfo.GetDescriptionAttributeText();

                        var propertyBuilder = builder.Property(propertyName);

                        propertyBuilder.HasColumnName(propertyName.ToSnakeCase());

                        propertyBuilder.HasComment(descriptionMember);
                    });
                }
            );
        });
    }

    #endregion
}