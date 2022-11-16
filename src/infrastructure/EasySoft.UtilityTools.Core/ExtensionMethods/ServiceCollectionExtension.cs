using EasySoft.UtilityTools.Core.Interfaces;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
public static class ServiceCollectionExtension
{
    private static readonly ConcurrentDictionary<string, char> RegisteredModels = new();

    /// <summary>
    /// HasRegistered
    /// </summary>
    /// <param name="_"></param>
    /// <param name="modelName"></param>
    /// <returns></returns>
    public static bool HasRegistered(this IServiceCollection _, string modelName)
    {
        return !RegisteredModels.TryAdd(modelName.ToLower(), '1');
    }

    /// <summary>
    /// ReplaceConfiguration
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ReplaceConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        return services.Replace(ServiceDescriptor.Singleton(configuration));
    }

    /// <summary>
    /// GetConfiguration
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IConfiguration GetConfiguration(this IServiceCollection services)
    {
        var hostBuilderContext = services.GetSingletonInstanceOrNull<HostBuilderContext>();

        if (hostBuilderContext?.Configuration is IConfigurationRoot instance)
            return instance;

        return services.GetSingletonInstance<IConfiguration>();
    }

    /// <summary>
    /// GetWebApiRegistrar
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IDependencyRegistrar GetWebApiRegistrar(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IDependencyRegistrar>();
    }

    private static T? GetSingletonInstanceOrNull<T>(this IServiceCollection services) where T : class
    {
        var instance = services.FirstOrDefault(d => d.ServiceType == typeof(T))?.ImplementationInstance;

        if (instance is null)
            return null;

        return (T)instance;
    }

    private static T GetSingletonInstance<T>(this IServiceCollection services)
        where T : class
    {
        var instance = GetSingletonInstanceOrNull<T>(services);

        if (instance is null)
            throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);

        return instance;
    }
}