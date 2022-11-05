using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.MultiTenant;

namespace EasySoft.Core.EntityFramework.Contexts.Basic;

public abstract class TenantBasicContext : BasicContext
{
    protected TenantBasicContext(
        DbContextOptions options,
        IEntityConfigure entityConfigure
    ) : base(options, entityConfigure)
    {
    }

    public ITenant? Tenant { get; set; }

    protected sealed override void OnAdvanceModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingWithTenant(modelBuilder, Tenant);
        OnMoreModelCreating(modelBuilder);
    }

    protected abstract void OnModelCreatingWithTenant(ModelBuilder modelBuilder, ITenant? tenant);

    protected virtual void OnMoreModelCreating(ModelBuilder modelBuilder)
    {
    }
}