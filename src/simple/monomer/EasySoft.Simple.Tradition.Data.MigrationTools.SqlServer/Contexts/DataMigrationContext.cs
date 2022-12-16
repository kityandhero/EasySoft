namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Contexts;

public class DataMigrationContext : SqlServerContext
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