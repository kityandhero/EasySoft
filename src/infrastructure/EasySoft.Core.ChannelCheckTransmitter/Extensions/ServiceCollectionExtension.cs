using EasySoft.Core.ChannelCheckTransmitter.Producers;

namespace EasySoft.Core.ChannelCheckTransmitter.Extensions;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
internal static class ServiceCollectionExtension
{
    internal static IServiceCollection AddChannelCheckTransmitter(
        this IServiceCollection service
    )
    {
        service.AddSingleton<IChannelCheckProducer, ChannelCheckProducer>();

        return service;
    }
}