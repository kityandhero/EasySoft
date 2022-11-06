using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.EntityFramework.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.DomainDrivenDesign.Infrastructure.Contexts;

public class DataContext : SqlServerContext
{
    public DataContext(
        DbContextOptions options,
        IEntityConfigure entityConfigure
    ) : base(options, entityConfigure)
    {
    }
}