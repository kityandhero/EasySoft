using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.EntityFramework.MySql.Contexts;
using EasySoft.Simple.Tradition.Data.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.Tradition.Data.Contexts;

public class MySqlDataContext : MySqlContext
{
    public MySqlDataContext(
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