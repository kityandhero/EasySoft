using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using FlagAssist = EasySoft.Core.Exceptionless.Assists.FlagAssist;

namespace EasySoft.Core.Exceptionless.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceExceptionless(this WebApplicationBuilder builder)
    {
        if (!GeneralConfigAssist.GetExceptionlessSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                $"ExceptionlessSwitch: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );

            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAdvanceExceptionless)}()."
        );

        StartupConfigMessageAssist.AddConfig(
            $"ExceptionlessSwitch: enable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        if (FlagAssist.GetAdvanceExceptionlessInitializeWhetherCompleted()) return builder;

        builder.Services.AddAdvanceExceptionless();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>().SetName("").SetAction(application => application.UseExceptionless())
        );

        FlagAssist.SetAdvanceExceptionlessInitializeCompleted();

        StartupDescriptionMessageAssist.AddDescription(
            $"Exceptionless is in use, address is {GeneralConfigAssist.GetExceptionlessServerUrl()}."
        );

        return builder;
    }
}