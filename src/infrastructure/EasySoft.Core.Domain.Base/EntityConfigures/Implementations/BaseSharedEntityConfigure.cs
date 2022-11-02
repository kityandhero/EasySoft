using System.Reflection;
using EasySoft.Core.Domain.Base.Contexts.Interfaces;
using EasySoft.Core.Domain.Base.EntityConfigures.Interfaces;
using EasySoft.Core.Infrastructure.Operations;
using EasySoft.Core.Infrastructure.Operations.Interfaces;
using EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Core.Domain.Base.EntityConfigures.Implementations;

/// <summary>
///  适用于 EntityFramework Core 实体配置
/// </summary>
public abstract class BaseSharedEntityConfigure<TOperator> : IEntityConfigure<TOperator> where TOperator : IOperator
{
    private readonly IOperatorContext _operatorContext;

    private TOperator? _operator;

    protected BaseSharedEntityConfigure(IOperatorContext operatorContext)
    {
        _operatorContext = operatorContext;
    }

    protected abstract TOperator InitializeOperator();

    public TOperator GetOperator()
    {
        _operator ??= InitializeOperator();

        _operator.Id = _operatorContext.Id == 0 ? OperatorConstant.SystemOperatorIdentification : _operatorContext.Id;

        _operator.Name = _operatorContext.Name.IsNullOrEmpty() ? "system" : _operatorContext.Name;

        return _operator;
    }

    protected virtual Assembly GetCurrentAssembly()
    {
        return GetType().Assembly;
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

    public virtual void OnModelCreating(dynamic modelBuilder)
    {
        if (modelBuilder is not ModelBuilder builder)
            throw new ArgumentNullException(nameof(modelBuilder));

        var entityAssembly = GetCurrentAssembly();
        var assemblies = new List<Assembly> { entityAssembly, typeof(EventTracker).Assembly };

        var entityTypes = GetEntityTypes(entityAssembly).ToList();

        entityTypes.ForEach(t => builder.Entity(t));

        assemblies.ForEach(assembly => builder.ApplyConfigurationsFromAssembly(assembly));

        SetComment(modelBuilder, entityTypes);
    }

    #region Static

    private static void SetComment(ModelBuilder modelBuilder, IEnumerable<Type>? types)
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
                    var typeSummary = entityType.ClrType.GetSummary();

                    builder.HasComment(typeSummary);

                    entityType.GetProperties().ForEach(property =>
                    {
                        var propertyName = property.Name;
                        var memberSummary = entityType.ClrType.GetMember(propertyName).FirstOrDefault()?.GetSummary();

                        builder.Property(propertyName).HasComment(memberSummary);
                    });
                }
            );
        });
    }

    #endregion
}