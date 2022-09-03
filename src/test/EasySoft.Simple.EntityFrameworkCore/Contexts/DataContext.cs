using EasySoft.Simple.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.EntityFrameworkCore.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    // }
    //
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    // }

    public DbSet<Author> Authors { get; set; } = null!;

    public DbSet<Blog> Blogs { get; set; } = null!;
}