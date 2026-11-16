using EasySoft.Core.ErrorLogTransceiver.Assists;

namespace EasySoft.Core.ErrorLogTransceiver.Extensions;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
internal static class ServerCollectionExtensions
{
    internal static IServiceCollection AddErrorLogTransceiverConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new ErrorLogTransceiverConfigureAssist(configuration));

        return services;
    }
}