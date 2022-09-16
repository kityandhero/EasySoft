using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Exceptionless;
using Microsoft.AspNetCore.Builder;
using FlagAssist = EasySoft.Core.Exceptionless.Assists.FlagAssist;

namespace EasySoft.Core.Exceptionless.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceExceptionless(this WebApplicationBuilder builder)
    {
        if (!GeneralConfigAssist.GetExceptionlessSwitch())
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetMessage(
                        $"ExceptionlessSwitch: disable."
                    )
                    .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );

            return builder;
        }

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetMessage(
                    $"ExceptionlessSwitch: enable."
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        if (FlagAssist.GetAdvanceExceptionlessInitializeWhetherCompleted())
        {
            return builder;
        }

        builder.Services.AddAdvanceExceptionless();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>().SetName("").SetAction(application => application.UseExceptionless())
        );

        FlagAssist.SetAdvanceExceptionlessInitializeCompleted();

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetMessage(
                    $"Exceptionless is in use, address is {GeneralConfigAssist.GetExceptionlessServerUrl()}."
                )
        );

        return builder;
    }
}