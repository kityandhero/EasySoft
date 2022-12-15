using EasySoft.Core.MultiTenant;

namespace EasySoft.Core.EntityFramework.Contexts.ContextFactories;

/// <summary>
/// AdvanceTenantContextFactory
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AdvanceTenantContextFactory<T> : IDbContextFactory<T> where T : TenantBasicContext
{
    private readonly IDbContextFactory<T> _pooledFactory;
    private readonly ITenant? _tenant;

    /// <summary>
    /// AdvanceTenantContextFactory
    /// </summary>
    /// <param name="pooledFactory"></param>
    /// <param name="tenant"></param>
    protected AdvanceTenantContextFactory(
        IDbContextFactory<T> pooledFactory,
        ITenant? tenant
    )
    {
        _pooledFactory = pooledFactory;
        _tenant = tenant;
    }

    /// <summary>
    /// CreateDbContext
    /// </summary>
    /// <returns></returns>
    public T CreateDbContext()
    {
        var context = _pooledFactory.CreateDbContext();

        context.Tenant = _tenant;

        return context;
    }
}