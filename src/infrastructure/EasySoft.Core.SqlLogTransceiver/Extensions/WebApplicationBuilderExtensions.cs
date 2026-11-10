using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.SqlLogTransceiver.Consumers;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.SqlLogTransceiver.Extensions;

/// <summary>
/// 
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddSqlLogRabbitMQTransmitter = "e90a347617d04e4ea2a875162d4b530d";

    private const string IdentifierAddSqlLogRabbitMQOnlyProducer = "94a9396a86c84abab1b51f2d8a234884";

    private const string IdentifierAddSqlLogRabbitMQOnlyConsumer = "1b215f2f8f2e42a0bab4118e441d72d0";

    /// <summary>
    /// 配置远程SQL日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSqlLogRabbitMQTransceiver<TSqlLogConsumer>(
        this WebApplicationBuilder builder
    ) where TSqlLogConsumer : BaseSqlLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddSqlLogRabbitMQTransmitter))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddSqlLogRabbitMQTransceiver)}");

        StartupConfigMessageAssist.AddConfig(
            SqlLogSwitchAssist.GetCurrentSwitch()
                ? "RemoteSqlLogEnable: enable."
                : "RemoteSqlLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddSqlLogRabbitMQCore(builder.Configuration);
        builder.Services.AddSqlLogRabbitMQProducer(builder.Configuration);
        builder.Services.AddSqlLogRabbitMQConsumer<TSqlLogConsumer>(builder.Configuration, 1);

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseSqlLogProducer(); }
                )
        );

        return builder;
    }

    /// <summary>
    /// 仅装配SQL日志 RabbitMQ 通道发送者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddSqlLogRabbitMQOnlyProducer(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddSqlLogRabbitMQOnlyProducer))
        {
            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            SqlLogSwitchAssist.GetCurrentSwitch()
                ? "RemoteSqlLogEnable: enable."
                : "RemoteSqlLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddSqlLogRabbitMQCore(builder.Configuration);
        builder.Services.AddSqlLogRabbitMQProducer(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// 仅装配SQL日志 RabbitMQ 通道消费者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="instances">消费者最大数</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddSqlLogRabbitMQOnlyConsumer<TSqlLogConsumer>(
        this WebApplicationBuilder builder,
        int instances = 1
    ) where TSqlLogConsumer : BaseSqlLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddSqlLogRabbitMQOnlyConsumer))
        {
            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            SqlLogSwitchAssist.GetCurrentSwitch()
                ? "RemoteSqlLogEnable: enable."
                : "RemoteSqlLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddSqlLogRabbitMQCore(builder.Configuration);
        builder.Services.AddSqlLogRabbitMQConsumer<TSqlLogConsumer>(builder.Configuration, instances);

        return builder;
    }
}