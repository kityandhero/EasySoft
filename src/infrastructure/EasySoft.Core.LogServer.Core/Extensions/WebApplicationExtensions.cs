using EasySoft.Core.Infrastructure.Configures;
using EasySoft.UtilityTools.Core.Assists;

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

        ApplicationConfigurator.OnApplicationStart += DoErrorLogSendExperiment;

        return application;
    }

    private static void DoErrorLogSendExperiment(IServiceProvider serviceProvider)
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

        AutofacAssist.Instance.Resolve<IErrorLogProducer>().Send(
            errorLogExchange
        );

        if (isDevelopment)
            LogAssist.Prompt(
                $"Execute errorLog send experiment end."
            );
    }
}