using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.MultiTenant;

namespace EasySoft.Core.EntityFramework.Contexts.Basic;

/// <summary>
/// TenantBasicContext
/// </summary>
public abstract class TenantBasicContext : BasicContext
{
    /// <summary>
    /// TenantBasicContext
    /// </summary>
    /// <param name="options"></param>
    /// <param name="entityConfigure"></param>
    protected TenantBasicContext(
        DbContextOptions options,
        IEntityConfigure entityConfigure
    ) : base(options, entityConfigure)
    {
    }

    /// <summary>
    /// Tenant
    /// </summary>
    public ITenant? Tenant { get; set; }

    /// <summary>
    /// OnAdvanceModelCreating
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected sealed override void OnAdvanceModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingWithTenant(modelBuilder, Tenant);
        OnMoreModelCreating(modelBuilder);
    }

    /// <summary>
    /// OnModelCreatingWithTenant
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="tenant"></param>
    protected abstract void OnModelCreatingWithTenant(ModelBuilder modelBuilder, ITenant? tenant);

    /// <summary>
    /// OnMoreModelCreating
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected virtual void OnMoreModelCreating(ModelBuilder modelBuilder)
    {
    }
}