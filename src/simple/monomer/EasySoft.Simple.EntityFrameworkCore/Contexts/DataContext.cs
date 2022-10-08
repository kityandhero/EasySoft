using EasySoft.Core.EntityFramework.Contexts.Basic;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.EntityFrameworkCore.Contexts;

public class DataContext : BasicContext
{
    public DataContext(
        DbContextOptions options
    ) : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; } = null!;

    public DbSet<Blog> Blogs { get; set; } = null!;

    public DbSet<Post> Posts { get; set; } = null!;
}