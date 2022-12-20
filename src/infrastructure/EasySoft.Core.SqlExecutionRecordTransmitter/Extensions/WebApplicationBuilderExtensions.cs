using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Extensions;

/// <summary>
/// 
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddSqlExecutionRecordTransmitter = "209646f2-bf76-4e7b-87e2-fde64eb9ee6b";

    /// <summary>
    /// 配置远程普通日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSqlExecutionRecordTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddSqlExecutionRecordTransmitter))
            return builder;

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddSqlExecutionRecordTransmitter)}");

        builder.Services.AddSqlExecutionRecordTransmitter();

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(
                    application => { application.UseSqlExecutionRecordProducer(); }
                )
        );

        return builder;
    }
}