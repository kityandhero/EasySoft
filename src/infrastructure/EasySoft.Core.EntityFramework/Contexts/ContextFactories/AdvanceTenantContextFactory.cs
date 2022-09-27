using EasySoft.Core.EntityFramework.Contexts.Basic;
using EasySoft.Core.MultiTenant;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Core.EntityFramework.Contexts.ContextFactories;

public abstract class AdvanceTenantContextFactory<T> : IDbContextFactory<T> where T : AdvanceTenantContextBase
{
    private readonly IDbContextFactory<T> _pooledFactory;
    private readonly ITenant? _tenant;

    public AdvanceTenantContextFactory(
        IDbContextFactory<T> pooledFactory,
        ITenant? tenant
    )
    {
        _pooledFactory = pooledFactory;
        _tenant = tenant;
    }

    public T CreateDbContext()
    {
        var context = _pooledFactory.CreateDbContext();

        context.Tenant = _tenant;

        return context;
    }
}