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

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceUrls)}()"
        );

        FlagAssist.StartupUrls.Clear();
        FlagAssist.StartupDisplayUrls.Clear();

        var httpPost = GeneralConfigAssist.GetHttpPost();
        var httpsPost = GeneralConfigAssist.GetHttpsPost();

        if (httpPost <= 0 && httpsPost <= 0)
            throw new Exception(
                $"one of HttpPost or HttpsPost must bu greater than 0, please set httpPost or httpsPost in {GeneralConfigAssist.GetConfigFileInfo()}."
            );

        if (httpPost > 0)
        {
            FlagAssist.StartupUrls.Add($"http://*:{httpPost}");
            FlagAssist.StartupDisplayUrls.Add($"http://localhost:{httpPost}");

            StartupConfigMessageAssist.AddConfig(
                $"HttpPost: {httpPost}."
            );
        }

        if (httpsPost > 0)
        {
            FlagAssist.StartupUrls.Add($"https://*:{httpsPost}");
            FlagAssist.StartupDisplayUrls.Add($"https://localhost:{httpsPost}");

            StartupConfigMessageAssist.AddConfig(
                $"HttpsPost: {httpsPost}."
            );
        }

        builder.WebHost.UseUrls(FlagAssist.StartupUrls.ToArray());

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access application at {FlagAssist.StartupDisplayUrls.Join(" ")}."
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