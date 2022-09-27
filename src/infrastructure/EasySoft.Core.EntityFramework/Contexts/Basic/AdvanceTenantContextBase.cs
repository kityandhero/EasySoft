using EasySoft.Core.MultiTenant;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.EntityFramework.Contexts.Basic;

public abstract class AdvanceTenantContextBase : AdvanceContextBase
{
    public ITenant? Tenant { get; set; }

    protected AdvanceTenantContextBase(
        DbContextOptions options
    ) : base(options)
    {
    }

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingWithTenant(modelBuilder, Tenant);
        OnMoreModelCreating(modelBuilder);
    }

    protected abstract void OnModelCreatingWithTenant(ModelBuilder modelBuilder, ITenant? tenant);

    protected virtual void OnMoreModelCreating(ModelBuilder modelBuilder)
    {
    }
}