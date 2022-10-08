using EasySoft.Core.MultiTenant;

namespace EasySoft.Core.EntityFramework.Contexts.ContextFactories;

public abstract class AdvanceTenantContextFactory<T> : IDbContextFactory<T> where T : TenantBasicContext
{
    private readonly IDbContextFactory<T> _pooledFactory;
    private readonly ITenant? _tenant;

    protected AdvanceTenantContextFactory(
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