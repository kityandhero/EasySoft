using EasySoft.Core.PrepareStartWork.PrepareWorks;

namespace EasySoft.Core.PrepareStartWork.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceUrls = "3745f94a-8439-488f-a38c-c8f93dd79932";

    private const string UniqueIdentifierAddCovertInjection = "1CE0086F-567F-4838-9611-55819A9A549B";

    public static WebApplicationBuilder AddAdvanceUrls(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceUrls))
            return builder;

        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceUrls)}()"
        );

        var urls = GeneralConfigAssist.GetUrls();

        if (string.IsNullOrWhiteSpace(urls))
        {
            StartupConfigMessageAssist.AddConfig(
                "Urls in generalConfig.json has not setting, suggest setting it with number or url, there will be better development experience."
            );

            return builder;
        }

        var urlsAdjust = urls.IsInt() ? $"http://localhost:{urls}" : urls;

        FlagAssist.StartupUrls = urlsAdjust.Split(",").ToListFilterNullOrWhiteSpace()
            .ForEach(o => o.IsInt() ? $"http://localhost:{o}" : o)
            .ToListFilterNullOrWhiteSpace();

        builder.WebHost.UseUrls(FlagAssist.StartupUrls.ToArray());

        StartupConfigMessageAssist.AddConfig(
            $"Startup urls: {FlagAssist.StartupUrls.Join(" ")}."
        );

        return builder;
    }

    /// <summary>
    /// 注入框架的启动预处理任务，启动时自动执行, 仅允许配置一次
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddCovertInjection(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddCovertInjection))
            return builder;

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<PrepareCovertStartWork>().As<IPrepareCovertStartWork>().SingleInstance();
        });

        return builder;
    }

    /// <summary>
    /// 注入定制的启动预处理任务，注入任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddPrepareStartWorkInjection<T>(
        this WebApplicationBuilder builder
    ) where T : IPrepareStartWork
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<IPrepareStartWork>().SingleInstance();
        });

        return builder;
    }
}