using EasySoft.Core.ErrorLogTransmitter.Producers;

namespace EasySoft.Core.ErrorLogTransmitter.Extensions;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
internal static class ServiceCollectionExtension
{
    internal static IServiceCollection AddErrorLogTransmitter(
        this IServiceCollection service
    )
    {
        service.AddSingleton<IErrorLogProducer, ErrorLogProducer>();

        return service;
    }
}