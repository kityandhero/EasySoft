using EasySoft.Core.Permission.Server.Entities;

namespace EasySoft.Core.Permission.Server.EntityConfigures;

/// <summary>
/// 已统合的实体配置, 将调用 Items 下的 各项配置
/// </summary>
public class IntegrationEntityConfigure : BaseEntityConfigure
{
    protected override IEnumerable<Assembly> GetEntityAssemblies()
    {
        return new List<Assembly> { typeof(RoleGroup).Assembly };
    }
}