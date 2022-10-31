using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Gateway.Ocelot.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceOcelot(
        this WebApplicationBuilder builder,
        bool useOcelotConfigFile = true
    )
    {
        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(
                    $"Execute {nameof(AddAdvanceOcelot)}()."
                )
        );

        builder.Services.AddAdvanceOcelot(useOcelotConfigFile);

        return builder;
    }
}