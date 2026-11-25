using EasySoft.Core.AppSecurityVerification.Assists;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.AppSecurityVerification.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// AddAppSecurityServerConfigureAssist
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    internal static IServiceCollection AddAppSecurityServerConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new AppSecurityServerConfigureAssist(configuration));

        return services;
    }
}