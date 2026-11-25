using EasySoft.Core.PermissionVerification.Assists;

namespace EasySoft.Core.PermissionVerification.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// AddAuthorizationConfigureAssist
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    internal static IServiceCollection AddAuthorizationConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new AuthorizationConfigureAssist(configuration));

        return services;
    }
}