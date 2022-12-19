using EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Extensions;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtension
{
    internal static IServiceCollection AddSqlExecutionRecordTransmitter(
        this IServiceCollection service
    )
    {
        service.AddSingleton<ISqlExecutionRecordProducer, SqlExecutionRecordProducer>();

        return service;
    }
}