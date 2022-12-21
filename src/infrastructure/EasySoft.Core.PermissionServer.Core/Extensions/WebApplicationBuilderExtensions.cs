using EasySoft.Core.PermissionServer.Core.Subscribers;

namespace EasySoft.Core.PermissionServer.Core.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// add permission server core logic
    /// </summary>
    /// <param name="builder"></param>    
    /// <returns></returns>
    internal static WebApplicationBuilder AddPermissionServerCore(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddPermissionServerCore)}."
        );

        builder.AddCapSubscriber<AccessWayExchangeSubscriber>();

        return builder;
    }
}