using EasySoft.Core.Infrastructure.Assists;
using Grpc.Net.ClientFactory;
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
            $"{nameof(AddAdvanceGrpc)}."
        );

        builder.Services.AddAdvanceGrpc();

        return builder;
    }

    /// <summary>
    /// AddAdvanceGrpcClient
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceGrpcClient<TGrpcClient>(
        this WebApplicationBuilder builder
    ) where TGrpcClient : class
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceGrpcClient)}."
        );

        builder.Services.AddAdvanceGrpcClient<TGrpcClient>();

        return builder;
    }

    /// <summary>
    /// AddAdvanceGrpcClient
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceGrpcClient<TGrpcClient>(
        this WebApplicationBuilder builder,
        Action<GrpcClientFactoryOptions> action
    ) where TGrpcClient : class
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceGrpcClient)}<{typeof(TGrpcClient).Name}>."
        );

        builder.Services.AddAdvanceGrpcClient<TGrpcClient>(action);

        return builder;
    }
}