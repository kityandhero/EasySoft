using EasySoft.Core.ChannelCheckTransceiver.Assists;

namespace EasySoft.Core.ChannelCheckTransceiver.Extensions;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
internal static class ServerCollectionExtensions
{
    internal static IServiceCollection AddChannelCheckTransceiverConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new ChannelCheckTransceiverConfigureAssist(configuration));

        return services;
    }
}