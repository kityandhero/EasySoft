using EasySoft.UtilityTools.Standard.Entities.Implements;

namespace EasySoft.Core.LogServer.Core.Extensions;

/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    internal static WebApplication UseLogSendExperiment(
        this WebApplication application
    )
    {
        return application
            .UseErrorLogSendExperiment()
            .UseGeneralLogSendExperiment()
            .UseSqlExecutionRecordSendExperiment();
    }

    /// <summary>
    /// 错误日志发送实验
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static WebApplication UseErrorLogSendExperiment(
        this WebApplication application
    )
    {
        if (!GeneralConfigAssist.GetRemoteErrorLogSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseErrorLogSendExperiment)}."
        );

        ApplicationConfigure.OnApplicationStart += async (serviceProvider) =>
        {
            await DoErrorLogSendExperiment(serviceProvider);
        };

        return application;
    }

    /// <summary>
    /// 一般日志发送实验
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static WebApplication UseGeneralLogSendExperiment(
        this WebApplication application
    )
    {
        if (!GeneralConfigAssist.GetRemoteGeneralLogSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseGeneralLogSendExperiment)}."
        );

        ApplicationConfigure.OnApplicationStart += async (serviceProvider) =>
        {
            await DoGeneralLogSendExperiment(serviceProvider);
        };

        return application;
    }

    /// <summary>
    /// Sql日志发送实验
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static WebApplication UseSqlExecutionRecordSendExperiment(
        this WebApplication application
    )
    {
        if (!GeneralConfigAssist.GetRemoteSqlExecutionRecordSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseSqlExecutionRecordSendExperiment)}."
        );

        ApplicationConfigure.OnApplicationStart += async (serviceProvider) =>
        {
            await DoSqlExecutionRecordSendExperiment(serviceProvider);
        };

        return application;
    }

    private static async Task DoErrorLogSendExperiment(IServiceProvider serviceProvider)
    {
        var environment = serviceProvider.GetService<IWebHostEnvironment>();

        var isDevelopment = environment.IsDevelopment();

        var errorLogExchange = new ErrorLogExchange
        {
            Ignore = 1
        };

        if (isDevelopment)
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

            logger.LogAdvancePrompt(
                $"Execute errorLog send experiment start."
            );

            logger.LogAdvancePrompt(
                $"Experiment ErrorLog -> \"Ignore\":\"1\"."
            );
        }

        await serviceProvider.GetRequiredService<IErrorLogProducer>().SendAsync(
            errorLogExchange
        );

        if (isDevelopment)
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

            logger.LogAdvancePrompt(
                $"Execute errorLog send experiment end."
            );
        }
    }

    private static async Task DoGeneralLogSendExperiment(IServiceProvider serviceProvider)
    {
        var environment = serviceProvider.GetService<IWebHostEnvironment>();

        var isDevelopment = environment.IsDevelopment();

        var generalLogExchange = new GeneralLogExchange
        {
            Ignore = 1
        };

        if (isDevelopment)
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

            logger.LogAdvancePrompt(
                $"Execute generalLog send experiment start."
            );

            logger.LogAdvancePrompt(
                $"Experiment GeneralLog -> \"Ignore\":\"1\"."
            );
        }

        await serviceProvider.GetRequiredService<IGeneralLogProducer>().SendAsync(
            generalLogExchange
        );

        if (isDevelopment)
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

            logger.LogAdvancePrompt(
                $"Execute generalLog send experiment end."
            );
        }
    }

    private static async Task DoSqlExecutionRecordSendExperiment(IServiceProvider serviceProvider)
    {
        var environment = serviceProvider.GetService<IWebHostEnvironment>();

        var isDevelopment = environment.IsDevelopment();

        var exchange = new SqlExecutionRecordExchange
        {
            Ignore = 1
        };

        if (isDevelopment)
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

            logger.LogAdvancePrompt(
                $"Execute sqlExecutionRecord send experiment start."
            );

            logger.LogAdvancePrompt(
                $"Experiment SqlExecutionRecord -> \"Ignore\":\"1\"."
            );
        }

        await serviceProvider.GetRequiredService<ISqlExecutionRecordProducer>().SendAsync(
            exchange
        );

        if (isDevelopment)
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

            logger.LogAdvancePrompt(
                $"Execute sqlExecutionRecord send experiment end."
            );
        }
    }
}