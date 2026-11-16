using EasySoft.Core.GeneralLogTransceiver.Assists;

namespace EasySoft.Core.GeneralLogTransceiver.Extensions;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
internal static class ServerCollectionExtensions
{
    internal static IServiceCollection AddGeneralLogTransceiverConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new GeneralLogTransceiverConfigureAssist(configuration));

        return services;
    }
}