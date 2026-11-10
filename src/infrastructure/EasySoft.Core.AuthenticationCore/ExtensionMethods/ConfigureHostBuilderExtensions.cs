using EasySoft.Core.AuthenticationCore.Operators;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// ConfigureHostBuilderExtensions
/// </summary>
public static class ConfigureHostBuilderExtensions
{
    /// <summary>
    /// 注入操作员
    /// </summary>
    /// <returns></returns>
    public static ConfigureHostBuilder AddOperator<T>(
        this ConfigureHostBuilder configureHostBuilder
    ) where T : ActualOperator, IActualOperator
    {
        configureHostBuilder.ConfigureServices(services => { services.AddScoped<IActualOperator, T>(); });

        return configureHostBuilder;
    }
}