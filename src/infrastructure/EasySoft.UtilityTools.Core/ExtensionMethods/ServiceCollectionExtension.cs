using System.Collections.Concurrent;
using EasySoft.UtilityTools.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class ServiceCollectionExtension
{
    private static readonly ConcurrentDictionary<string, char> RegisteredModels = new();

    public static bool HasRegistered(this IServiceCollection _, string modelName)
    {
        return !RegisteredModels.TryAdd(modelName.ToLower(), '1');
    }

    public static IServiceCollection ReplaceConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        return services.Replace(ServiceDescriptor.Singleton(configuration));
    }

    public static IConfiguration GetConfiguration(this IServiceCollection services)
    {
        var hostBuilderContext = services.GetSingletonInstanceOrNull<HostBuilderContext>();

        if (hostBuilderContext?.Configuration is IConfigurationRoot instance)
            return instance;

        return services.GetSingletonInstance<IConfiguration>();
    }

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