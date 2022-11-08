using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Grpc.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// AddAdvanceGrpc
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceGrpc(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceGrpc)}()."
        );

        builder.Services.AddAdvanceGrpc();

        return builder;
    }
}