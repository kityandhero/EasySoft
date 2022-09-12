using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.OpenTelemetry.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceCap(this WebApplicationBuilder builder)
    {
        // builder.Services.AddOpenTelemetryTracing()
        //     .AddZipkinExporter();

        return builder;
    }
}