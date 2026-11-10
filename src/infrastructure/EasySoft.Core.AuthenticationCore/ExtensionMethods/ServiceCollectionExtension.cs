using EasySoft.Core.AuthenticationCore.Operators;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
public static class ServiceCollectionExtension
{
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