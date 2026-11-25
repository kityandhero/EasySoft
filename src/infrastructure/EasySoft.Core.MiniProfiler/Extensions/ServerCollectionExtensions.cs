using EasySoft.Core.MiniProfiler.Assists;
using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.Core.MiniProfiler.Extensions;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入 MiniProfile
    /// </summary>   
    /// <param name="services">构建器</param>
    /// <param name="configuration">配置集</param>
    /// <param name="actionOptions">选项配置器</param>
    /// <param name="actionMiniProfilerBuilder"></param>
    /// <returns></returns>      
    public static IServiceCollection AddAdvanceMiniProfile(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<MiniProfilerOptions> actionOptions = null,
        Action<IMiniProfilerBuilder> actionMiniProfilerBuilder = null
    )
    {
        services.AddSingleton(new MiniProFileConfigureAssist(configuration));

        var miniProFileConfigValue = MiniProFileConfigureAssist.GetMiniProFile(configuration);

        StartupConfigMessageAssist.AddConfig(
            miniProFileConfigValue.ToArrowDescription()
        );

        if (!miniProFileConfigValue.Value)
        {
            return null;
        }

        var miniProfilerBuilder = services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";

                // 设定弹出窗口的位置是左下角
                options.PopupRenderPosition = RenderPosition.BottomLeft;

                // 设定在弹出的明细窗口里会显式 Time With Children 这列
                options.PopupShowTimeWithChildren = true;

                options.ColorScheme = ColorScheme.Auto;

                actionOptions?.Invoke(options);
            }
        );

        actionMiniProfilerBuilder?.Invoke(miniProfilerBuilder);

        return services;
    }
}