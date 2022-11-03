using EasySoft.Core.EntityFramework.Contexts.Basic;

namespace EasySoft.Core.EntityFramework.MySql.Contexts;

public class DataContext : BasicContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnAdvanceModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCharSet("utf8mb4 ");

        OnModelCreating(modelBuilder);
    }
}