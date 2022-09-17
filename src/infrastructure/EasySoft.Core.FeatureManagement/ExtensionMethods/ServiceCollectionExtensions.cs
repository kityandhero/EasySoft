using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace EasySoft.Core.FeatureManagement.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Feature Management
    /// </summary>  
    /// <remarks>
    /// It is suitable for the scenario where the switch function is required
    /// </remarks>
    public static IServiceCollection AddAdvanceFeatureManagement(
        this IServiceCollection serviceCollection,
        Action<IFeatureManagementBuilder>? action = null
    )
    {
        var builder = serviceCollection.AddFeatureManagement();

        action?.Invoke(builder);

        return serviceCollection;
    }

    /// <summary>
    /// Feature Management
    /// </summary>  
    /// <remarks>
    /// It is suitable for the scenario where the switch function is required
    /// </remarks>
    public static IServiceCollection AddAdvanceFeatureManagement(
        this IServiceCollection serviceCollection,
        IConfiguration configuration,
        Action<IFeatureManagementBuilder>? action = null
    )
    {
        var builder = serviceCollection.AddFeatureManagement(configuration);

        action?.Invoke(builder);

        return serviceCollection;
    }
}