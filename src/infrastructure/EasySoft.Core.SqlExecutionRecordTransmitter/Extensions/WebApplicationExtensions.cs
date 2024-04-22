using EasySoft.Core.SqlExecutionRecordTransmitter.Producers;
using EasySoft.UtilityTools.Standard.Assists;

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
        if (!SqlLogSwitchAssist.GetCurrentSwitch())
        {
            return application;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseSqlExecutionRecordProducer)}."
        );

        ApplicationConfigure.AddTimer(
            SqlLogInnerQueue.SendInterval,
            (serviceProvider, e) =>
            {
                var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

                if (SqlLogInnerQueue.GetQueue().Count <= 0)
                {
                    var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

                    logger.LogAdvancePrompt(
                        $"None sql execution record need send."
                    );

                    return;
                }

                if (environment.IsDevelopment())
                {
                    var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

                    logger.LogAdvancePrompt(
                        $"Exec send sql execution record, count {SqlLogInnerQueue.GetQueue().Count}."
                    );
                }

                var sqlExecutionRecordProducer = serviceProvider.GetRequiredService<ISqlLogProducer>();

                while (SqlLogInnerQueue.GetQueue().TryDequeue(out var sqlLog))
                {
                    sqlExecutionRecordProducer.SendAsync(sqlLog);
                }
            }
        );

        return application;
    }
}