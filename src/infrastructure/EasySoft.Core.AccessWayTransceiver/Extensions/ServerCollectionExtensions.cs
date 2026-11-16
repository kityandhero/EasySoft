using EasySoft.Core.AccessWayTransceiver.Assists;

namespace EasySoft.Core.AccessWayTransceiver.Extensions;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
internal static class ServerCollectionExtensions
{
    internal static IServiceCollection AddAccessWayTransceiverConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new AccessWayTransceiverConfigureAssist(configuration));

        return services;
    }
}