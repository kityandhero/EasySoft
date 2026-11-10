using EasySoft.Core.ErrorLogTransceiver.Extensions;
using EasySoft.Core.GeneralLogTransceiver.Extensions;
using EasySoft.Core.LogServer.Core.Consumers;
using EasySoft.Core.SqlLogTransceiver.Extensions;

namespace EasySoft.Core.LogServer.Core.Extensions;

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
    internal static WebApplicationBuilder AddLogServerCore(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddLogServerCore)}."
        );

        builder.AddErrorLogRabbitMQOnlyConsumer<ErrorLogConsumer>();
        builder.AddGeneralLogRabbitMQOnlyConsumer<GeneralLogConsumer>();
        builder.AddSqlLogRabbitMQOnlyConsumer<SqlLogConsumer>();

        return builder;
    }
}