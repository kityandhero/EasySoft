using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using Masuit.Tools;
using Assembly = System.Reflection.Assembly;

namespace EasySoft.Core.EntityFramework.EntityConfigures.Implementations;

/// <summary>
///  适用于 EntityFramework Core 实体配置
/// </summary>
public class EntityConfigure : IEntityConfigure
{
    private readonly ISet<Assembly> _assemblies = new HashSet<Assembly>();

    /// <inheritdoc />
    public IEnumerable<Assembly> GetAssemblies()
    {
        return _assemblies;
    }

    /// <inheritdoc />
    public IEntityConfigure AddAssembly(Assembly assembly)
    {
        _assemblies.Add(assembly);

        return this;
    }

    /// <inheritdoc />
    public IEntityConfigure AddRangeAssemblies(IEnumerable<Assembly> assemblies)
    {
        _assemblies.AddRange(assemblies);

        return this;
    }

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    public virtual void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assemblies = new List<Assembly> { typeof(EventTracker).Assembly };

        assemblies.AddRange(GetAssemblies());

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

        EnumerableExtensions.ForEach(entityTypes, entityType =>
        {
            modelBuilder.Entity(
                entityType.Name,
                builder =>
                {
                    builder.ToTable(entityType.ClrType.Name.ToSnakeCase());

                    var descriptionTable = entityType.ClrType.GetDescriptionAttributeText();

                    if (!string.IsNullOrWhiteSpace(descriptionTable)) builder.HasComment(descriptionTable);

                    EnumerableExtensions.ForEach(entityType.GetProperties(), property =>
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