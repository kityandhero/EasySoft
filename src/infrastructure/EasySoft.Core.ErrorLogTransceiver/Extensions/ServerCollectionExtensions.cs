using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.ErrorLogTransceiver.Assists;
using EasySoft.Core.ErrorLogTransceiver.Collectors;
using EasySoft.Core.ErrorLogTransceiver.Services;
using EasySoft.UtilityTools.Standard.Producers;

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

    internal static IServiceCollection AddErrorLogCollector(
        this IServiceCollection services,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    )
    {
        services.AddSingleton(serviceProvider =>
            {
                var options = new DateRollingUtf8FileRecorderOptions
                {
                    PathTemplate = "records/error_logs/{Date:yyyyMMdd}.log",
                    MaxDirectorySizeBytes = 10L * 1024 * 1024
                };

                action?.Invoke(options);

                var channel = serviceProvider.GetRequiredService<IApplicationChannel>();

                var transceiverConfigureAssist = serviceProvider.GetRequiredService<
                    ErrorLogTransceiverConfigureAssist
                >();

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                var useTransmissionConfigValue = transceiverConfigureAssist.GetUseTransmission();

                return new ErrorLogCollector(
                    loggerFactory,
                    channel,
                    useTransmissionConfigValue.Value ? serviceProvider.GetRequiredService<IErrorLogProducer>() : null,
                    Options.Create(options),
                    transceiverConfigureAssist.GetUseLocalRecord().Value,
                    useTransmissionConfigValue.Value
                );
            }
        );

        services.AddHostedService<ErrorLogCollectorHostedService>();

        services.Configure<HostOptions>(o => { o.ShutdownTimeout = TimeSpan.FromSeconds(15); });

        return services;
    }
}