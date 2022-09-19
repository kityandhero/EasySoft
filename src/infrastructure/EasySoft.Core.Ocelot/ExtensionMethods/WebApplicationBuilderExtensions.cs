using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Ocelot.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceOcelot(
        this WebApplicationBuilder builder,
        bool useOcelotConfigFile = true
    )
    {
        builder.Services.AddAdvanceOcelot(useOcelotConfigFile);

        return builder;
    }
}