using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Grpc.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdvanceGrpc(
        this IServiceCollection serviceCollection
    )
    {
        var builder = serviceCollection.AddGrpc();

        builder.Services.AddGrpc();

        return serviceCollection;
    }
}