namespace EasySoft.Core.DependencyInjection.IocAssists;

public class ServiceProviderAssist
{
    public static readonly ServiceProviderAssist Instance = new();

    private IServiceProvider? _root;
    private IServiceProviderIsService? _isService;
    private IServiceProviderIsKeyedService? _isKeyedService;

    public void SetProvider(IServiceProvider provider)
    {
        if (_root != null)
        {
            throw new Exception("provider has been set, it disallow set more than once.");
        }

        _root = provider;
        _isService = provider.GetService<IServiceProviderIsService>();
        _isKeyedService = provider.GetService<IServiceProviderIsKeyedService>();
    }

    public IServiceProvider GetProvider()
    {
        if (_root == null)
        {
            throw new Exception("Provider is null, please assign a value before use it.");
        }

        return _root;
    }

    // ── 解析：无 keyed ──
    public T Resolve<T>() where T : notnull
    {
        return GetProvider().GetRequiredService<T>();
    }

    public object Resolve(Type serviceType)
    {
        return GetProvider().GetRequiredService(serviceType);
    }

    // ── 解析：带 keyed（.NET 8+ 原生支持）──
    public T ResolveKeyed<T>(string serviceKey) where T : notnull
    {
        return GetProvider().GetRequiredKeyedService<T>(serviceKey);
    }

    public object ResolveKeyed(string serviceKey, Type serviceType)
    {
        return GetProvider().GetRequiredKeyedService(serviceType, serviceKey);
    }

    // ── 解析：带运行时参数（仅对非 keyed 服务可用）──
    public T ResolveWithParameters<T>(params object[] parameters) where T : notnull
    {
        return ActivatorUtilities.CreateInstance<T>(GetProvider(), parameters);
    }

    public object ResolveWithParameters(Type implementationType, params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance(
            GetProvider(),
            implementationType,
            parameters
        );
    }

    // ── 注册检查 ──
    public bool IsRegistered<T>() where T : notnull
    {
        return _isService?.IsService(typeof(T)) ?? false;
    }

    public bool IsRegistered(Type serviceType)
    {
        return _isService?.IsService(serviceType) ?? false;
    }

    public bool IsRegisteredWithKey<T>(string serviceKey) where T : notnull
    {
        return _isKeyedService?.IsKeyedService(typeof(T), serviceKey) ?? false;
    }

    public bool IsRegisteredWithKey(string serviceKey, Type serviceType)
    {
        return _isKeyedService?.IsKeyedService(serviceType, serviceKey) ?? false;
    }

    // ── 子作用域（对应 Autofac.BeginLifetimeScope）──
    public IServiceScope CreateScope()
    {
        return GetProvider().CreateScope();
    }
}