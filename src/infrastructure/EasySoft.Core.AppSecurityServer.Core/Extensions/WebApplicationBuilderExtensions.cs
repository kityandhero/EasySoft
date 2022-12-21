using EasySoft.Core.Infrastructure.Queues;
using EasySoft.Core.SqlExecutionRecordTransmitter.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.AppSecurityServer.Core.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// add permission server core logic
    /// </summary>
    /// <param name="builder"></param>    
    /// <returns></returns>
    internal static WebApplicationBuilder AddPermissionServerCore(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddPermissionServerCore)}."
        );

        ApplicationConfigure.AddTimer(
            60000,
            (serviceProvider, e) =>
            {
                var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

                if (environment.IsDevelopment())
                {
                    var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

                    logger.LogAdvancePrompt(
                        $"Exec send sql execution record, count {SqlLogInnerQueue.GetQueue().Count}."
                    );
                }

                var sqlExecutionRecordProducer = serviceProvider.GetRequiredService<ISqlExecutionRecordProducer>();

                while (SqlLogInnerQueue.GetQueue().TryDequeue(out var sqlExecutionRecord))
                    sqlExecutionRecordProducer.SendAsync(sqlExecutionRecord);
            }
        );

        return builder;
    }
}