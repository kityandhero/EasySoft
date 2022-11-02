using EasySoft.Core.EntityFramework.SqlServer.Contexts;
using EasySoft.Simple.Tradition.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.Tradition.Data.Contexts;

public class DataContext : BaseContext
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