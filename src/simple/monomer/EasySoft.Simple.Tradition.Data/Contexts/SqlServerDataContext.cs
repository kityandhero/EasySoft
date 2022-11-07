using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.EntityFramework.SqlServer.Contexts;
using EasySoft.Simple.Tradition.Data.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.Tradition.Data.Contexts;

public class SqlServerDataContext : SqlServerContext
{
    public SqlServerDataContext(
        DbContextOptions options,
        IEntityConfigure entityConfigure
    ) : base(options, entityConfigure)
    {
    }

    protected sealed override void OnSeedCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}