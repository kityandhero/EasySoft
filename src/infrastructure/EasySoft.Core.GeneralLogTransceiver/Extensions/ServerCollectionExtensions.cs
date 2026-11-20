using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.GeneralLogTransceiver.Assists;
using EasySoft.Core.GeneralLogTransceiver.Collectors;
using EasySoft.Core.GeneralLogTransceiver.Services;
using EasySoft.UtilityTools.Standard.Producers;
using Microsoft.Extensions.Hosting;

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

    internal static IServiceCollection AddGeneralLogCollector(
        this IServiceCollection services,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    )
    {
        services.AddSingleton(serviceProvider =>
            {
                var options = new DateRollingUtf8FileRecorderOptions
                {
                    PathTemplate = "records/general_logs/{Date:yyyyMMdd}.log",
                    MaxDirectorySizeBytes = 10L * 1024 * 1024
                };

                action?.Invoke(options);

                var channel = serviceProvider.GetRequiredService<IApplicationChannel>();

                var transceiverConfigureAssist = serviceProvider
                    .GetRequiredService<GeneralLogTransceiverConfigureAssist>();

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                return new GeneralLogCollector(
                    loggerFactory,
                    channel,
                    serviceProvider.GetRequiredService<IGeneralLogProducer>(),
                    Options.Create(options),
                    transceiverConfigureAssist.GetUseLocalRecord().Value,
                    transceiverConfigureAssist.GetUseTransmission().Value
                );
            }
        );

        services.AddHostedService<GeneralLogCollectorHostedService>();

        services.Configure<HostOptions>(o => { o.ShutdownTimeout = TimeSpan.FromSeconds(15); });

        return services;
    }
}