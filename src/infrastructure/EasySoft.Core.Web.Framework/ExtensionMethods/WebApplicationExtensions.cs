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

        if (ApplicationConfigure.GetAllAreas().Any())
        {
            var areaAdjust = ApplicationConfigure.GetAllAreas()
                .Where(o => !string.IsNullOrWhiteSpace(o.Remove(" ")))
                .ToList();

            if (areaAdjust.Any())
            {
                StartupConfigMessageAssist.AddTraceDivider();

                StartupConfigMessageAssist.AddConfig(
                    $"Areas: {ApplicationConfigure.GetAllAreas().Join(",")}"
                );

                StartupDescriptionMessageAssist.AddPrompt(
                    $"Areas: {ApplicationConfigure.GetAllAreas().Join(",")}"
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
        var extraActions = ApplicationConfigure.GetAllEndpointRouteBuilderExtraActions().ToList();

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
            ApplicationConfigure.DoAfterApplicationStart(application.Services);
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
            ApplicationConfigure.DoWhenApplicationStopping(application.Services);
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
            ApplicationConfigure.OnApplicationStart += serviceProvider =>
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
        ApplicationConfigure.OnApplicationStart += _ =>
        {
            var timers = ApplicationConfigure.GetTimers();

            if (!timers.Any()) return;

            if (application.Environment.IsDevelopment())
                application.Logger.LogAdvancePrompt(
                    $"Timers ({timers.Count}) will start."
                );

            timers.ForEach(t => { t.Start(); });
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
        ApplicationConfigure.OnApplicationStopping += _ =>
        {
            var timers = ApplicationConfigure.GetTimers();

            if (!timers.Any()) return;

            if (application.Environment.IsDevelopment())
                application.Logger.LogAdvancePrompt(
                    $"Timers ({timers.Count}) will stop and dispose."
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