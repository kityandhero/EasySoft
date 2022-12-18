using EasySoft.Core.Infrastructure.Configures;

namespace EasySoft.Core.LogDashboard.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceLogDashboard = "fad8d668-1074-425f-9eb5-a3a57c87c7ea";

    /// <summary>
    /// 注入定制的静态文件配置，诸如任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceLogDashboard(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceLogDashboard))
            return builder;

        builder.Services.AddLogDashboard();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("UseAdvanceLogDashboard")
                .SetAction(application => { application.UseAdvanceLogDashboard(); })
        );

        return builder;
    }
}