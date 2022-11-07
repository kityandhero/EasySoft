using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.EntityFramework.MySql.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.DomainDrivenDesign.Infrastructure.Contexts;

public class DataContext : MySqlContext
{
    public DataContext(
        DbContextOptions options,
        IEntityConfigure entityConfigure
    ) : base(options, entityConfigure)
    {
    }
}