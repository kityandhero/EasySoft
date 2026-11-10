using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Infrastructure.Queues;
using EasySoft.Core.SqlLogTransceiver.Producers;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.SqlLogTransceiver.Extensions;

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