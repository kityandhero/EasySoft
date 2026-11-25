using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Core.Interfaces;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
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
    /// 注入 Host 应用配置
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new ApplicationConfigureAssist(configuration));

        return services;
    }

    /// <summary>
    /// AddJsonSerializerSettings
    /// </summary>
    /// <param name="services">服务集</param>
    /// <returns></returns>   
    public static IServiceCollection AddJsonSerializerSettings(
        this IServiceCollection services
    )
    {
        services.AddSingleton(JsonConvertAssist.CreateJsonSerializerSettings(false));

        return services;
    }

    internal static IServiceCollection AddAdvanceDefaultApplicationChannel(
        this IServiceCollection services
    )
    {
        services.AddAdvanceApplicationChannel(
            ApplicationChannel.DefaultChannel
        );

        return services;
    }

    public static IServiceCollection AddAdvanceApplicationChannel(
        this IServiceCollection services,
        IChannel channel
    )
    {
        var applicationChannel = new ApplicationChannel().SetChannel(channel);

        services.AddSingleton<IApplicationChannel>(applicationChannel);

        StartupDescriptionMessageAssist.AddPrompt(
            $"ApplicationChannel name is {channel.Name}, identification is {(string.IsNullOrWhiteSpace(channel.Value) ? "\"\"" : channel.Value)}."
        );

        return services;
    }

    /// <summary>
    /// ReplaceConfiguration
    /// </summary>
    /// <param name="services">服务集</param>
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
    /// <param name="services">服务集</param>
    /// <returns></returns>
    public static IConfiguration GetConfiguration(this IServiceCollection services)
    {
        var hostBuilderContext = services.GetSingletonInstanceOrNull<HostBuilderContext>();

        if (hostBuilderContext?.Configuration is IConfigurationRoot instance)
        {
            return instance;
        }

        return services.GetSingletonInstance<IConfiguration>();
    }

    /// <summary>
    /// GetDependencyRegistrar
    /// </summary>
    /// <param name="services">服务集</param>
    /// <returns></returns>
    public static IDependencyRegistrar GetDependencyRegistrar(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IDependencyRegistrar>();
    }

    private static T GetSingletonInstanceOrNull<T>(this IServiceCollection services) where T : class
    {
        var instance = services.FirstOrDefault(d => d.ServiceType == typeof(T))?.ImplementationInstance;

        if (instance is null)
        {
            return null;
        }

        return (T)instance;
    }

    private static T GetSingletonInstance<T>(this IServiceCollection services)
        where T : class
    {
        var instance = GetSingletonInstanceOrNull<T>(services);

        if (instance is null)
        {
            throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
        }

        return instance;
    }
}