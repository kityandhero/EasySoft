using DotNetCore.CAP;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace EasySoft.Simple.EntityFrameworkCore.Contexts;

public class DataContext : AdvanceContextBase
{
    public DataContext(
        DbContextOptions options
    ) : base(options)
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

    public DbSet<Post> Posts { get; set; } = null!;

    public override Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override IDbContextTransaction BeginTransactionWithPersistenceTarget(ICapPublisher publisher,
        bool autoCommit = false)
    {
        throw new NotImplementedException();
    }
}