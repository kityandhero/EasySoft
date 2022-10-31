using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace EasySoft.Core.FeatureManagement.ExtensionMethods;

/// <summary>
/// Feature Management
/// </summary>  
/// <remarks>
/// It is suitable for the scenario where the switch function is required
/// </remarks>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Feature Management
    /// </summary>  
    /// <remarks>
    /// It is suitable for the scenario where the switch function is required
    /// </remarks>
    public static WebApplicationBuilder AddAdvanceFeatureManagement(
        this WebApplicationBuilder builder,
        Action<IFeatureManagementBuilder>? action = null
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAdvanceFeatureManagement)}()."
        );

        builder.Services.AddAdvanceFeatureManagement(action);

        return builder;
    }

    /// <summary>
    /// Feature Management
    /// </summary>  
    /// <remarks>
    /// It is suitable for the scenario where the switch function is required
    /// </remarks>
    public static WebApplicationBuilder AddAdvanceFeatureManagement(
        this WebApplicationBuilder builder,
        IConfiguration configuration,
        Action<IFeatureManagementBuilder>? action = null
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAdvanceFeatureManagement)}()."
        );

        builder.Services.AddAdvanceFeatureManagement(configuration, action);

        return builder;
    }
}