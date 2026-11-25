using EasySoft.Core.Gateway.Ocelot.ConfigAssist;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Startup;

namespace EasySoft.Core.Gateway.Ocelot.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceOcelot(
        this WebApplicationBuilder builder,
        bool useOcelotConfigFile = true
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceOcelot)}."
        );

        OcelotConfigAssist.Init();

        builder.Services.AddAdvanceOcelot(useOcelotConfigFile);

        if (HostEnvironmentAssist.IsDevelopment())
        {
            WebApplicationConfigure.AddEndpointRouteBuilderExtraAction(
                new ExtraAction<IEndpointRouteBuilder>()
                    .SetName("")
                    .SetAction(endpoints => { endpoints.MapOcelotConfigFile(); })
            );
        }

        return builder;
    }
}