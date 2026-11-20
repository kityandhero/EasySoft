using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.ChannelCheckTransceiver.Assists;
using EasySoft.Core.ChannelCheckTransceiver.Collectors;
using EasySoft.Core.ChannelCheckTransceiver.Services;
using EasySoft.UtilityTools.Standard.Producers;
using Microsoft.Extensions.Hosting;

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

    internal static IServiceCollection AddChannelCheckCollector(
        this IServiceCollection services,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    )
    {
        services.AddSingleton(serviceProvider =>
            {
                var options = new DateRollingUtf8FileRecorderOptions
                {
                    PathTemplate = "records/channel_check_logs/{Date:yyyyMMdd}.log",
                    MaxDirectorySizeBytes = 10L * 1024 * 1024
                };  

                action?.Invoke(options);

                var channel = serviceProvider.GetRequiredService<IApplicationChannel>();

                var transceiverConfigureAssist = serviceProvider
                    .GetRequiredService<ChannelCheckTransceiverConfigureAssist>();

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                var useTransmissionConfigValue = transceiverConfigureAssist.GetUseTransmission();

                return new ChannelCheckCollector(
                    loggerFactory,
                    channel,
                    useTransmissionConfigValue.Value
                        ? serviceProvider.GetRequiredService<IChannelCheckProducer>()
                        : null,
                    Options.Create(options),
                    transceiverConfigureAssist.GetUseLocalRecord().Value,
                    useTransmissionConfigValue.Value
                );
            }
        );

        services.AddHostedService<ChannelCheckCollectorHostedService>();

        services.Configure<HostOptions>(o => { o.ShutdownTimeout = TimeSpan.FromSeconds(15); });

        return services;
    }
}