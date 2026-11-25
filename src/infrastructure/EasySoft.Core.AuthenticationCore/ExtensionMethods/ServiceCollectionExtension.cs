using EasySoft.Core.AuthenticationCore.Assists;
using EasySoft.Core.AuthenticationCore.Operators;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// AddAuthenticationConfigureAssist
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    internal static IServiceCollection AddAuthenticationConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new AuthenticationConfigureAssist(configuration));

        return services;
    }

    /// <summary>
    /// 注入操作员
    /// </summary>
    /// <returns></returns>
    public static IServiceCollection AddOperator<T>(
        this IServiceCollection services
    ) where T : ActualOperator, IActualOperator
    {
        services.AddScoped<IActualOperator, T>();
        return services;
    }
}