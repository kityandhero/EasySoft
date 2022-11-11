namespace EasySoft.Core.Grpc.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdvanceGrpc(
        this IServiceCollection serviceCollection
    )
    {
        serviceCollection.AddGrpc();

        return serviceCollection;
    }

    public static IServiceCollection AddAdvanceGrpcClient<TGrpcClient>(
        this IServiceCollection serviceCollection
    ) where TGrpcClient : class
    {
        serviceCollection.AddGrpcClient<TGrpcClient>();

        return serviceCollection;
    }

    public static IServiceCollection AddAdvanceGrpcClient<TGrpcClient>(
        this IServiceCollection serviceCollection,
        Action<GrpcClientFactoryOptions> action
    ) where TGrpcClient : class
    {
        serviceCollection.AddGrpcClient<TGrpcClient>(action);

        return serviceCollection;
    }
}