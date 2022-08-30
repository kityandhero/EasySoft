using EasySoft.Core.Infrastructure.Assists;
using LogDashboard;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.LogDashboard.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入定制的静态文件配置，诸如任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceLogDashboard(
        this WebApplicationBuilder builder
    )
    {
        if (FlagAssist.GetLogDashboardSwitch())
        {
            return builder;
        }

        builder.Services.AddLogDashboard();

        FlagAssist.SetLogDashboardSwitchOpen();

        ApplicationConfigActionAssist.AddWebApplicationAction(application => { application.UseAdvanceLogDashboard(); });

        return builder;
    }
}