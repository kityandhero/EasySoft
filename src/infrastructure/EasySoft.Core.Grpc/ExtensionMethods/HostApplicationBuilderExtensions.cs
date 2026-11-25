using EasySoft.UtilityTools.Core.Assists;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Grpc.ExtensionMethods;

public static class HostApplicationBuilderExtensions
{
    /// <summary>
    /// AddAdvanceGrpc
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddAdvanceGrpc(
        this IHostApplicationBuilder builder
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
    /// <param name="builder">构建器</param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddAdvanceGrpcClient<TGrpcClient>(
        this IHostApplicationBuilder builder
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
    /// <param name="builder">构建器</param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddAdvanceGrpcClient<TGrpcClient>(
        this IHostApplicationBuilder builder,
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