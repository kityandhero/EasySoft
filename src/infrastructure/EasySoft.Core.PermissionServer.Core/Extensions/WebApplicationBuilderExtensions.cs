using EasySoft.Core.AccessWayTransceiver.Extensions;
using EasySoft.Core.PermissionServer.Core.Consumers;
using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.Core.PermissionServer.Core.Extensions;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// add permission server core logic
    /// </summary>
    /// <param name="builder">构建器</param>    
    /// <returns></returns>
    internal static WebApplicationBuilder AddPermissionServerCore(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddPermissionServerCore)}."
        );

        builder.AddAccessWayRabbitMQOnlyConsumer<AccessWayExchangeSubscriber>();

        return builder;
    }
}