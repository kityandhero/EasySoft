using System.Reflection;
using EasySoft.Core.Domain.Base.Entities.Implementations;
using EasySoft.Core.Domain.Base.Entities.Interfaces;
using EasySoft.Core.EntityFramework.EntityConfigures.Implementations;
using EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;

namespace EasySoft.Core.Domain.Share.EntityConfigures.Implementations;

/// <summary>
/// 适用于 EntityFramework Core 领域实体配置
/// </summary>
public abstract class BaseDomainEntityConfigure : BaseEntityConfigure
{
    protected override IEnumerable<Type> GetEntityTypes(Assembly assembly)
    {
        var typeList = assembly.GetTypes().Where(m =>
            m.FullName != null
            && (typeof(IAggregateRoot).IsAssignableFrom(m) || typeof(DomainEntity).IsAssignableFrom(m))
            && !m.IsAbstract);

        return typeList.Append(typeof(EventTracker));
    }
}