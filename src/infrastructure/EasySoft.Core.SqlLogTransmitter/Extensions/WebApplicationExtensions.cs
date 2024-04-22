using EasySoft.Core.SqlLogTransmitter.Producers;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.SqlLogTransmitter.Extensions;

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
    internal static WebApplication UseSqlLogProducer(
        this WebApplication application
    )
    {
        if (!SqlLogSwitchAssist.GetCurrentSwitch())
        {
            return application;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseSqlLogProducer)}."
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

                var sqlLogProducer = serviceProvider.GetRequiredService<ISqlLogProducer>();

                while (SqlLogInnerQueue.GetQueue().TryDequeue(out var sqlLog))
                {
                    sqlLogProducer.SendAsync(sqlLog);
                }
            }
        );

        return application;
    }
}