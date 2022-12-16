namespace EasySoft.Simple.Tradition.Data.MigrationTools.MySql.Contexts;

public class DataMigrationContext : MySqlContext
{
    public DataMigrationContext(
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