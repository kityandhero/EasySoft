using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Extensions;

/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// UseScanPermission
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static WebApplication UseSqlExecutionRecordProducer(
        this WebApplication application
    )
    {
        if (!GeneralConfigAssist.GetRemoteSqlExecutionRecordSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseSqlExecutionRecordProducer)}."
        );

        ApplicationConfigure.AddTimer(
            3000,
            (serviceProvider, e) =>
            {
                if (SqlLogInnerQueue.GetQueue().Count <= 0) return;

                var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

                if (environment.IsDevelopment())
                {
                    var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

                    logger.LogAdvancePrompt(
                        $"Exec send sql execution record, count {SqlLogInnerQueue.GetQueue().Count}."
                    );
                }

                var sqlExecutionRecordProducer = serviceProvider.GetRequiredService<ISqlExecutionRecordProducer>();

                while (SqlLogInnerQueue.GetQueue().TryDequeue(out var sqlExecutionRecord))
                    sqlExecutionRecordProducer.SendAsync(sqlExecutionRecord);
            }
        );

        return application;
    }
}