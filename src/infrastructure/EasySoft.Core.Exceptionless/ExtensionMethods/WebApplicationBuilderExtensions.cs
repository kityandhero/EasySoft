using EasySoft.Core.Exceptionless.Assists;
using EasySoft.Core.Infrastructure.Configures;
using FlagAssist = EasySoft.Core.Exceptionless.Assists.FlagAssist;

namespace EasySoft.Core.Exceptionless.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceExceptionless(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionlessConfigureAssist(builder.Configuration);

        if (!ExceptionlessConfigureAssist.GetExceptionlessTeleport(builder.Configuration))
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

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>().SetName("").SetAction(application => application.UseExceptionless())
        );

        FlagAssist.SetAdvanceExceptionlessInitializeCompleted();

        StartupDescriptionMessageAssist.AddPrompt(
            $"Exceptionless is in use, address is {ExceptionlessConfigureAssist.GetExceptionlessServerUrl(builder.Configuration)}."
        );

        return builder;
    }
}