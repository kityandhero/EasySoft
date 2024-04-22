using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.SqlLogTransmitter.Extensions;

/// <summary>
/// 
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddSqlLogTransmitter = "209646f2-bf76-4e7b-87e2-fde64eb9ee6b";

    /// <summary>
    /// 配置远程普通日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSqlLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddSqlLogTransmitter))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddSqlLogTransmitter)}");

        StartupConfigMessageAssist.AddConfig(
            SqlLogSwitchAssist.GetCurrentSwitch()
                ? "RemoteSqlLogEnable: enable."
                : "RemoteSqlLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddSqlLogTransmitter();

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(
                    application => { application.UseSqlLogProducer(); }
                )
        );

        return builder;
    }
}