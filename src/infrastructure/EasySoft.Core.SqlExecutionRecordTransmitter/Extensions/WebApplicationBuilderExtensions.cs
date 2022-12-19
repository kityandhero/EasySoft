namespace EasySoft.Core.SqlExecutionRecordTransmitter.Extensions;

/// <summary>
/// 
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程普通日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSqlExecutionRecordTransmitter(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddSqlExecutionRecordTransmitter();

        return builder;
    }
}