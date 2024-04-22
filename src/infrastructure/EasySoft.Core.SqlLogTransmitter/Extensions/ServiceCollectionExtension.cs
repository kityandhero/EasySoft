using EasySoft.Core.SqlLogTransmitter.Producers;

namespace EasySoft.Core.SqlLogTransmitter.Extensions;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtension
{
    internal static IServiceCollection AddSqlLogTransmitter(
        this IServiceCollection service
    )
    {
        service.AddSingleton<ISqlLogProducer, SqlLogProducer>();

        return service;
    }
}