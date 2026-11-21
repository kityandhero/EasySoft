using EasySoft.Core.AccessWayTransceiver.Assists;
using EasySoft.Core.AccessWayTransceiver.Collectors;
using EasySoft.Core.AccessWayTransceiver.Services;
using EasySoft.Core.AdvanceFileRecord;
using EasySoft.UtilityTools.Standard.Producers;

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

    internal static IServiceCollection AddAccessWayCollector(
        this IServiceCollection services,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    )
    {
        services.AddSingleton(serviceProvider =>
            {
                var options = new DateRollingUtf8FileRecorderOptions
                {
                    PathTemplate = "records/access_way_logs/{Date:yyyyMMdd}.log",
                    MaxDirectorySizeBytes = 10L * 1024 * 1024
                };

                action?.Invoke(options);

                var channel = serviceProvider.GetRequiredService<IApplicationChannel>();

                var transceiverConfigureAssist = serviceProvider
                    .GetRequiredService<AccessWayTransceiverConfigureAssist>();

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                var useTransmissionConfigValue = transceiverConfigureAssist.GetUseTransmission();

                return new AccessWayCollector(
                    loggerFactory,
                    channel,
                    useTransmissionConfigValue.Value
                        ? serviceProvider.GetRequiredService<IAccessWayProducer>()
                        : null,
                    Options.Create(options),
                    transceiverConfigureAssist.GetUseLocalRecord().Value,
                    useTransmissionConfigValue.Value
                );
            }
        );

        services.AddHostedService<AccessWayCollectorHostedService>();

        services.Configure<HostOptions>(o => { o.ShutdownTimeout = TimeSpan.FromSeconds(15); });

        return services;
    }
}