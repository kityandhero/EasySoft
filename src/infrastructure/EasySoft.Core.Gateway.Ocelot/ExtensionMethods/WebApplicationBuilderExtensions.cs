using EasySoft.Core.Gateway.Ocelot.ConfigAssist;

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

        if (EnvironmentAssist.IsDevelopment() && AuxiliaryConfigure.PromptConfigFileInfo)
            ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
                new ExtraAction<IEndpointRouteBuilder>()
                    .SetName("")
                    .SetAction(endpoints => { endpoints.MapOcelotConfigFile(); })
            );

        return builder;
    }
}