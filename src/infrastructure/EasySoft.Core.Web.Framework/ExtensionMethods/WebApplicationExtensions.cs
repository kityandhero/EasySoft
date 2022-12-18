namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationExtensions
{
    /// <summary>
    /// build route areas
    /// </summary>  
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseAdvanceMapControllers(
        this WebApplication application
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceMapControllers)}."
        );

        application.UseMvc();

        var startMessage = new StartupMessage()
            .SetLevel(LogLevel.Trace)
            .SetMessage(
                UtilityTools.Standard.ConstCollection.ApplicationStartExtraEndpointMessageStartDivider
            );

        if (ApplicationConfigurator.GetAllAreas().Any())
        {
            var areaAdjust = ApplicationConfigurator.GetAllAreas()
                .Where(o => !string.IsNullOrWhiteSpace(o.Remove(" ")))
                .ToList();

            if (areaAdjust.Any())
            {
                StartupConfigMessageAssist.AddTraceDivider();

                StartupConfigMessageAssist.AddConfig(
                    $"Areas: {ApplicationConfigurator.GetAllAreas().Join(",")}"
                );

                StartupDescriptionMessageAssist.AddPrompt(
                    $"Areas: {ApplicationConfigurator.GetAllAreas().Join(",")}"
                );

                application.UseEndpoints(endpoints =>
                {
                    WeaveExtraAction(endpoints, startMessage);

                    areaAdjust.ForEach(o =>
                    {
                        endpoints.MapAreaControllerRoute(
                            o,
                            o,
                            "{area:exists}/{controller}/{action}"
                        );
                    });

                    endpoints.MapControllerRoute(
                        "areaRoute",
                        "{area:exists}/{controller}/{action}"
                    );

                    endpoints.MapControllerRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}"
                    );
                });
            }
            else
            {
                application.UseEndpoints(endpoints =>
                {
                    WeaveExtraAction(endpoints, startMessage);

                    endpoints.MapControllerRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}"
                    );
                });
            }
        }
        else
        {
            application.UseEndpoints(endpoints =>
            {
                WeaveExtraAction(endpoints, startMessage);

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        return application;
    }

    /// <summary>
    /// 织入扩展的 IEndpointRouteBuilder Action
    /// </summary>
    private static void WeaveExtraAction(
        IEndpointRouteBuilder endpoint,
        IStartupMessage startNormalMessageAssist
    )
    {
        var extraActions = ApplicationConfigurator.GetAllEndpointRouteBuilderExtraActions().ToList();

        if (extraActions.Count <= 0) return;

        StartupEndPointExtraActionMessageAssist.Add(startNormalMessageAssist);

        for (var i = 0; i < extraActions.Count; i++)
        {
            var extraAction = extraActions[i];

            var action = extraAction.GetAction();

            if (action == null) continue;

            var name = extraAction.GetName();

            if (!string.IsNullOrWhiteSpace(name))
                StartupEndPointExtraActionMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"{i + 1}: {extraAction.GetName()}"
                        )
                );

            action(endpoint);
        }
    }

    /// <summary>
    /// 注册应用启动后自定义任务
    /// </summary>  
    /// <param name="application"></param>
    /// <returns></returns>
    internal static WebApplication RegisterWorks(
        this WebApplication application
    )
    {
        return application
            .BindTimers()
            .RegisterStartWorks()
            .RegisterStoppingWorks()
            .BindStartWorkCompleteNotify();
    }

    /// <summary>
    /// 注册应用启动完成后自定义任务
    /// </summary>  
    /// <param name="application"></param>
    /// <returns></returns>
    private static WebApplication RegisterStartWorks(
        this WebApplication application
    )
    {
        application.Lifetime.ApplicationStarted.Register(() =>
        {
            ApplicationConfigurator.DoAfterApplicationStart(application.Services);
        });

        return application;
    }

    /// <summary>
    /// 注册应用停止时自定义任务
    /// </summary>  
    /// <param name="application"></param>
    /// <returns></returns>
    private static WebApplication RegisterStoppingWorks(
        this WebApplication application
    )
    {
        application.Lifetime.ApplicationStopping.Register(() =>
        {
            ApplicationConfigurator.DoWhenApplicationStopping(application.Services);
        });

        return application;
    }

    /// <summary>
    /// 绑定应用启动后自定义任务执行完毕通知
    /// </summary>  
    /// <param name="application"></param>
    /// <returns></returns>
    private static WebApplication BindStartWorkCompleteNotify(
        this WebApplication application
    )
    {
        if (application.Environment.IsDevelopment())
            ApplicationConfigurator.OnApplicationStart += serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                loggerFactory?.CreateLogger<object>().LogAdvancePrompt(
                    "Execute work after application End."
                );
            };

        return application;
    }

    /// <summary>
    /// 绑定定时器触发
    /// </summary>
    private static WebApplication BindTimers(
        this WebApplication application
    )
    {
        return application
            .BindTimersStart()
            .BindTimersStop();
    }

    /// <summary>
    /// 绑定定时器启动触发
    /// </summary>
    private static WebApplication BindTimersStart(
        this WebApplication application
    )
    {
        //应用启动后启动定时器
        ApplicationConfigurator.OnApplicationStart += _ =>
        {
            var timers = ApplicationConfigurator.GetTimers();

            if (!timers.Any()) return;

            if (application.Environment.IsDevelopment())
                application.Logger.LogAdvancePrompt(
                    $"Times({timers.Count}) will start."
                );

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            tokenSource.Cancel();

            Task.Delay(
                    TimeSpan.FromSeconds(5),
                    token
                )
                .ContinueWith(
                    t => { timers.ForEach(t => { t.Start(); }); },
                    token
                );
        };

        return application;
    }

    /// <summary>
    /// 绑定定时器停止触发
    /// </summary>
    private static WebApplication BindTimersStop(
        this WebApplication application
    )
    {
        //停止定时器执行并释放资源
        ApplicationConfigurator.OnApplicationStopping += _ =>
        {
            var timers = ApplicationConfigurator.GetTimers();

            if (!timers.Any()) return;

            if (application.Environment.IsDevelopment())
                application.Logger.LogAdvancePrompt(
                    $"Times({timers.Count}) will stop and dispose."
                );

            timers.ForEach(t =>
            {
                t.Stop();

                t.Dispose();
            });
        };

        return application;
    }
}