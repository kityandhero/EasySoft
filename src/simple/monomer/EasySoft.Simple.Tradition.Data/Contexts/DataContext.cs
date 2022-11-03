using System.Reflection;
using EasySoft.Core.EntityFramework.SqlServer.Contexts;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Data.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.Tradition.Data.Contexts;

public class DataContext : BaseContext
{
    public DataContext(
        DbContextOptions options
    ) : base(options)
    {
    }

    protected sealed override List<Assembly> GetEntityTypeConfigureAssemblies()
    {
        return new List<Assembly>
        {
            typeof(Blog).Assembly
        };
    }

    protected sealed override void OnSeedCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Blog> Blogs { get; set; } = null!;

    public DbSet<Post> Posts { get; set; } = null!;
}