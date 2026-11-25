using EasySoft.Core.Exceptionless.Assists;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Startup;
using FlagAssist = EasySoft.Core.Exceptionless.Assists.FlagAssist;

namespace EasySoft.Core.Exceptionless.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceExceptionless(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionlessConfigureAssist(builder.Configuration);

        if (!ExceptionlessConfigureAssist.GetExceptionlessTeleport(builder.Configuration).Value)
        {
            StartupConfigMessageAssist.AddConfig(
                $"Exceptionless Teleport: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );

            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceExceptionless)}."
        );

        StartupConfigMessageAssist.AddConfig(
            "ExceptionlessSwitch: enable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        if (FlagAssist.GetAdvanceExceptionlessInitializeWhetherCompleted())
        {
            return builder;
        }

        builder.Services.AddAdvanceExceptionless(builder.Configuration);

        WebApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>().SetName("").SetAction(application => application.UseExceptionless())
        );

        FlagAssist.SetAdvanceExceptionlessInitializeCompleted();

        StartupDescriptionMessageAssist.AddPrompt(
            $"Exceptionless is in use, address is {ExceptionlessConfigureAssist.GetExceptionlessServerUrl(builder.Configuration)}."
        );

        return builder;
    }
}