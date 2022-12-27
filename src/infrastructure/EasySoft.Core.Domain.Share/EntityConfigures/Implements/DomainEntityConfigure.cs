using EasySoft.Core.Domain.Base.Entities.Implements;
using EasySoft.Core.EntityFramework.EntityConfigures.Implements;
using EasySoft.Core.Infrastructure.Entities.Implements;

namespace EasySoft.Core.Domain.Share.EntityConfigures.Implements;

/// <summary>
/// 适用于 EntityFramework Core 领域实体配置
/// </summary>
public abstract class DomainEntityConfigure : EntityConfigure
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