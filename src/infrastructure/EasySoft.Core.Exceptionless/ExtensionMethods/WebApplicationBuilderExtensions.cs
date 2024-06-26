﻿using EasySoft.Core.Infrastructure.Configures;
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
            $"{nameof(AddAdvanceExceptionless)}."
        );

        StartupConfigMessageAssist.AddConfig(
            "ExceptionlessSwitch: enable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        if (FlagAssist.GetAdvanceExceptionlessInitializeWhetherCompleted()) return builder;

        builder.Services.AddAdvanceExceptionless();

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>().SetName("").SetAction(application => application.UseExceptionless())
        );

        FlagAssist.SetAdvanceExceptionlessInitializeCompleted();

        StartupDescriptionMessageAssist.AddPrompt(
            $"Exceptionless is in use, address is {GeneralConfigAssist.GetExceptionlessServerUrl()}."
        );

        return builder;
    }
}