using EasySoft.Core.Hangfire.Assists;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Startup;

namespace EasySoft.Core.Hangfire.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddSwaggerGen = "3ed53dbb090f4a3d811319bb257176cd";

    public static WebApplicationBuilder AddAdvanceHangfire(this WebApplicationBuilder builder)
    {
        if (builder.HasRegistered(UniqueIdentifierAddSwaggerGen))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceHangfire)}."
        );

        builder.Services.AddSingleton(new HangfireConfigAssist(builder.Configuration));

        var storageTypeConfigValue = HangfireConfigAssist.GetStorageType(builder.Configuration);

        //启用Hangfire服务.
        builder.Services.AddHangfireServer();

        switch (storageTypeConfigValue.Value)
        {
            case "MemoryStorage":
                builder.Services.AddHangfire(x => x.UseStorage(new MemoryStorage()));
                break;

            case "SqlServer":
                var connectionConfigValue = HangfireConfigAssist.GetStorageConnection(builder.Configuration);

                builder.Services.AddHangfire(x => x.UseStorage(new SqlServerStorage(connectionConfigValue.Value)));

                break;

            default:
                throw new UnsupportedException(
                    $"Hangfire storage type is {storageTypeConfigValue.Value}."
                );
        }

        StartupDescriptionMessageAssist.AddPrompt(
            $"Hangfire storage type is {storageTypeConfigValue.Value}."
        );

        WebApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAdvanceHangfire(); })
        );

        return builder;
    }
}