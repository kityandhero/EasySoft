using EasySoft.Core.SqlLogTransceiver.Assists;

namespace EasySoft.Core.SqlLogTransceiver.Extensions;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
internal static class ServerCollectionExtensions
{
    internal static IServiceCollection AddSqlLogTransceiverConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new SqlLogTransceiverConfigureAssist(configuration));

        return services;
    }
}