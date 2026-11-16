using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Infrastructure.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// AddInfrastructureConfigureAssist
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    internal static IServiceCollection AddInfrastructureConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new InfrastructureConfigureAssist(configuration));

        return services;
    }

    /// <summary>
    /// AddJsonSerializerSettings
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    internal static IServiceCollection AddJsonSerializerSettings(
        this IServiceCollection services
    )
    {
        services.AddSingleton(JsonConvertAssist.CreateJsonSerializerSettings(false));

        return services;
    }
}