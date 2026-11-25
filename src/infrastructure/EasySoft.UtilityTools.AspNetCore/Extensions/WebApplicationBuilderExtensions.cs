using EasySoft.UtilityTools.AspNetCore.Options;

namespace EasySoft.UtilityTools.AspNetCore.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入定制的静态文件配置，诸如任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddStaticFileOptionsInjection<T>(
        this WebApplicationBuilder builder
    ) where T : AdvanceStaticFileOptions
    {
        builder.Services.AddSingleton<AdvanceStaticFileOptions, T>();

        return builder;
    }

    /// <summary>
    /// AddExtraNormalInjection
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static WebApplicationBuilder AddExtraNormalInjection(
        this WebApplicationBuilder builder,
        Action<IServiceCollection> action
    )
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        builder.Host.ConfigureServices(action);

        return builder;
    }
}