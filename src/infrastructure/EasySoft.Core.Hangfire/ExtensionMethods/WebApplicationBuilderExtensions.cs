using EasySoft.Core.Hangfire.Assists;
using EasySoft.Core.Hangfire.ConfigAssist;
using EasySoft.Core.Hangfire.Enums;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Exceptions;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Hangfire.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddSwaggerGen = "0fc40198-fa59-4f88-9d01-4ba2fbed9ae0";

    public static WebApplicationBuilder AddAdvanceHangfire(this WebApplicationBuilder builder)
    {
        if (builder.HasRegistered(UniqueIdentifierAddSwaggerGen))
            return builder;

        StartupDescriptionMessageAssist.AddTraceDivider();

        HangfireConfigAssist.Init();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceHangfire)}."
        );

        var hangfireSwitch = HangfireConfigAssist.GetSwitch();

        StartupConfigMessageAssist.AddConfig(
            $"Hangfire: {(!hangfireSwitch ? "disable" : "enable")}.",
            HangfireConfigAssist.GetConfigFileInfo()
        );

        if (!hangfireSwitch)
        {
            StartupDescriptionMessageAssist.AddPrompt(
                $"Hangfire is closed."
            );

            return builder;
        }

        var storageType = HangfireAssist.GetStorageType();

        //启用Hangfire服务.
        builder.Services.AddHangfireServer();

        switch (storageType)
        {
            case StorageType.MemoryStorage:
                builder.Services.AddHangfire(x => x.UseStorage(new MemoryStorage()));
                break;

            case StorageType.SqlServer:
                var connection = HangfireConfigAssist.GetStorageConnection();

                builder.Services.AddHangfire(x => x.UseStorage(new SqlServerStorage(connection)));
                break;

            default:
                throw new UnsupportedException(
                    $"Hangfire storage type is {storageType.ToString()}."
                );
        }

        StartupDescriptionMessageAssist.AddPrompt(
            $"Hangfire storage type is {storageType.ToString()}, available storage is {HangfireAssist.GetAllAvailableStorageType()}."
        );

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAdvanceHangfire(); })
        );

        return builder;
    }
}