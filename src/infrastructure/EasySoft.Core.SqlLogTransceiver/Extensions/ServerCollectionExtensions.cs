using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.SqlLogTransceiver.Assists;
using EasySoft.Core.SqlLogTransceiver.Collectors;
using EasySoft.Core.SqlLogTransceiver.Services;
using EasySoft.UtilityTools.Standard.Producers;
using Microsoft.Extensions.Hosting;

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

    internal static IServiceCollection AddSqlLogCollector(
        this IServiceCollection services,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    )
    {
        services.AddSingleton(serviceProvider =>
            {
                var options = new DateRollingUtf8FileRecorderOptions
                {
                    PathTemplate = "records/sql_logs/{Date:yyyyMMdd}.log",
                    MaxDirectorySizeBytes = 10L * 1024 * 1024
                };

                action?.Invoke(options);

                var channel = serviceProvider.GetRequiredService<IApplicationChannel>();

                var transceiverConfigureAssist = serviceProvider.GetRequiredService<
                    SqlLogTransceiverConfigureAssist
                >();

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                var useTransmissionConfigValue = transceiverConfigureAssist.GetUseTransmission();

                return new SqlLogCollector(
                    loggerFactory,
                    channel,
                    useTransmissionConfigValue.Value ? serviceProvider.GetRequiredService<ISqlLogProducer>() : null,
                    Options.Create(options),
                    transceiverConfigureAssist.GetUseLocalRecord().Value,
                    useTransmissionConfigValue.Value
                );
            }
        );

        services.AddHostedService<SqlLogCollectorHostedService>();

        services.Configure<HostOptions>(o => { o.ShutdownTimeout = TimeSpan.FromSeconds(15); });

        return services;
    }
}