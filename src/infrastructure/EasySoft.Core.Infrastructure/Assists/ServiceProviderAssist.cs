namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// 静态服务定位器。用于框架底层无法构造函数注入的场景（如工具类、静态初始化、第三方扩展点）。
/// <para>
/// 使用前提：在宿主启动阶段调用一次 <see cref="SetProvider"/>，之后全局只读。
/// 重复调用会抛异常，避免运行时替换容器导致单例撕裂。
/// </para>
/// <para>
/// 静态状态不可测试隔离。单元测试需要隔离时，用 <see cref="Reset"/> 在每个测试前后清理。
/// <see cref="Reset"/> 仅在 Debug 构建生效。
/// </para>
/// </summary>
public static class ServiceProviderAssist
{
    private static IServiceProvider _serviceProvider;
    private static IServiceProviderIsService _serviceProviderIsService;
    private static IServiceProviderIsKeyedService _serviceProviderIsKeyedService;

    /// <summary>
    /// 设置根容器。只允许调用一次，重复调用抛异常。
    /// 通常在 <c>builder.Build()</c> 之后立即调用，传入 <c>app.Services</c>。
    /// </summary>
    /// <param name="provider">宿主构建后的根 IServiceProvider。</param>
    /// <exception cref="InvalidOperationException">已经设置过 provider。</exception>
    /// <exception cref="ArgumentNullException"><paramref name="provider"/> 为 null。</exception>
    public static void SetProvider(IServiceProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        if (_serviceProvider is not null)
        {
            throw new InvalidOperationException(
                "Provider has been set; it disallows setting more than once."
            );
        }

        _serviceProvider = provider;

        // 这两个是可选的：IServiceProviderIsService / IServiceProviderIsKeyedService
        // 在某些最小化容器中可能未注册，GetService 返回 null，不影响 Resolve 能力。
        _serviceProviderIsService = provider.GetService<IServiceProviderIsService>();
        _serviceProviderIsKeyedService = provider.GetService<IServiceProviderIsKeyedService>();
    }

    /// <summary>获取根容器。未初始化时抛异常。</summary>
    /// <exception cref="InvalidOperationException">未调用 <see cref="SetProvider"/>。</exception>
    public static IServiceProvider GetProvider()
    {
        return _serviceProvider
               ?? throw new InvalidOperationException(
                   "Provider is null; call SetProvider before using it."
               );
    }

    /// <summary>解析非 Keyed 服务。</summary>
    public static T Resolve<T>() where T : notnull
    {
        return GetProvider().GetRequiredService<T>();
    }

    /// <summary>按类型解析非 Keyed 服务。</summary>
    public static object Resolve(Type serviceType)
    {
        return GetProvider().GetRequiredService(serviceType);
    }

    /// <summary>解析 Keyed 服务。.NET 8+ 原生支持。</summary>
    public static T ResolveKeyed<T>(string serviceKey) where T : notnull
    {
        return GetProvider().GetRequiredKeyedService<T>(serviceKey);
    }

    /// <summary>按类型解析 Keyed 服务。</summary>
    public static object ResolveKeyed(string serviceKey, Type serviceType)
    {
        return GetProvider().GetRequiredKeyedService(serviceType, serviceKey);
    }

    /// <summary>
    /// 带运行时参数构造实例。参数优先使用传入的 <paramref name="parameters"/>，
    /// 其余构造参数从容器解析。注意：此方法不解析 Keyed 参数。
    /// </summary>
    public static T ResolveWithParameters<T>(params object[] parameters) where T : notnull
    {
        return ActivatorUtilities.CreateInstance<T>(GetProvider(), parameters);
    }

    /// <summary>
    /// 带运行时参数构造实例，指定实现类型。参数优先使用传入的 <paramref name="parameters"/>，
    /// 其余构造参数从容器解析。
    /// </summary>
    public static object ResolveWithParameters(Type implementationType, params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance(
            GetProvider(),
            implementationType,
            parameters
        );
    }

    /// <summary>检查非 Keyed 服务是否已注册。未注册 <see cref="IServiceProviderIsService"/> 时返回 false。</summary>
    public static bool IsRegistered<T>() where T : notnull
    {
        return _serviceProviderIsService?.IsService(typeof(T)) ?? false;
    }

    /// <summary>按类型检查非 Keyed 服务是否已注册。</summary>
    public static bool IsRegistered(Type serviceType)
    {
        return _serviceProviderIsService?.IsService(serviceType) ?? false;
    }

    /// <summary>检查 Keyed 服务是否已注册。未注册 <see cref="IServiceProviderIsKeyedService"/> 时返回 false。</summary>
    public static bool IsRegisteredWithKey<T>(string serviceKey) where T : notnull
    {
        return _serviceProviderIsKeyedService?.IsKeyedService(typeof(T), serviceKey) ?? false;
    }

    /// <summary>按类型检查 Keyed 服务是否已注册。</summary>
    public static bool IsRegisteredWithKey(string serviceKey, Type serviceType)
    {
        return _serviceProviderIsKeyedService?.IsKeyedService(serviceType, serviceKey) ?? false;
    }

    /// <summary>创建子作用域。用于解析 Scoped 服务。</summary>
    public static IServiceScope CreateScope()
    {
        return GetProvider().CreateScope();
    }

    /// <summary>
    /// 重置静态状态。仅供单元测试使用，Debug 构建才编译进程序集。
    /// 测试用例应在 SetUp/TearDown 中调用，避免跨用例污染。
    /// </summary>
    [System.Diagnostics.Conditional("DEBUG")]
    public static void Reset()
    {
        _serviceProvider = null;
        _serviceProviderIsService = null;
        _serviceProviderIsKeyedService = null;
    }

    /// <summary>
    /// GetServerPath
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetServerPath(string path)
    {
        return GetProvider().GetRequiredService<IWebHostEnvironment>().ContentRootPath + path;
    }
}