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

        var applicationLifetime = application.Services.GetService<IHostApplicationLifetime>();

        applicationLifetime?.ApplicationStarted.Register(DoErrorLogSendExperiment);

        return application;
    }

    private static void DoErrorLogSendExperiment()
    {
        LogAssist.Prompt(
            $"Do ErrorLog send experiment."
        );

        AutofacAssist.Instance.Resolve<IErrorLogProducer>().Send(
            new ErrorLogExchange
            {
                Ignore = 1
            }
        );
    }
}