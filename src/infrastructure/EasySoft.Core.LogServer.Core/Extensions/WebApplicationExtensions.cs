namespace EasySoft.Core.LogServer.Core.Extensions;

/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
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
            LogAssist.Prompt(
                $"Execute errorLog send experiment start."
            );

            LogAssist.Prompt(
                $"Experiment ErrorLog -> \"Ignore\":\"1\"."
            );
        }

        await AutofacAssist.Instance.Resolve<IErrorLogProducer>().SendAsync(
            errorLogExchange
        );

        if (isDevelopment)
            LogAssist.Prompt(
                $"Execute errorLog send experiment end."
            );
    }
}