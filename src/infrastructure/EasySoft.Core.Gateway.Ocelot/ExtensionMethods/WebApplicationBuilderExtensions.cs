using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Gateway.Ocelot.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceOcelot(
        this WebApplicationBuilder builder,
        bool useOcelotConfigFile = true
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAdvanceOcelot)}()."
        );

        builder.Services.AddAdvanceOcelot(useOcelotConfigFile);

        return builder;
    }
}