using EasySoft.Core.EntityFramework.Contexts.Basic;
using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.Infrastructure.Operations.Interfaces;

namespace EasySoft.Core.EntityFramework.SqlServer.Contexts;

public class BaseContext : BasicContext
{
    public BaseContext(DbContextOptions options, IEntityConfigure entityConfigure) : base(
        options,
        entityConfigure
    )
    {
    }
}