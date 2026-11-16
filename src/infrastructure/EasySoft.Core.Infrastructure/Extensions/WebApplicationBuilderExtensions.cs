using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Options;

namespace EasySoft.Core.Infrastructure.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddInfrastructure = "2fd0664caeb34924b1b72a5bf374b3be";

    /// <summary>
    /// AddInfrastructure
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddInfrastructure(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddInfrastructure))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddInfrastructure)}");

        builder.Services.AddInfrastructureConfigureAssist(builder.Configuration);

        builder.Services.AddJsonSerializerSettings();

        return builder;
    }

    /// <summary>
    /// 注入定制的静态文件配置，诸如任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddStaticFileOptionsInjection<T>(
        this WebApplicationBuilder builder
    ) where T : AdvanceStaticFileOptions
    {
        builder.Services.AddSingleton<AdvanceStaticFileOptions, T>();

        return builder;
    }
}