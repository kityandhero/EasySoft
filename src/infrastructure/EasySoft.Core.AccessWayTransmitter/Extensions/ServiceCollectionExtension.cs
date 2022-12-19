using EasySoft.Core.AccessWayTransmitter.Producers;

namespace EasySoft.Core.AccessWayTransmitter.Extensions;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
internal static class ServiceCollectionExtension
{
    internal static IServiceCollection AddAccessWayTransmitter(
        this IServiceCollection service
    )
    {
        service.AddSingleton<IAccessWayProducer, AccessWayProducer>();

        return service;
    }
}