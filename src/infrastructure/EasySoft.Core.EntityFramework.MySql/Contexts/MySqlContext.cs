namespace EasySoft.Core.EntityFramework.MySql.Contexts;

public class MySqlContext : BasicContext
{
    public MySqlContext(DbContextOptions options, IEntityConfigure entityConfigure) : base(
        options,
        entityConfigure
    )
    {
    }

    protected override void OnAdvanceModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCharSet("utf8mb4 ");
    }
}