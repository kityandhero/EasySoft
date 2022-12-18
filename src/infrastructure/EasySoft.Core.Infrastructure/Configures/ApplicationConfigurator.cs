using EasySoft.Core.Infrastructure.Startup;
using Masuit.Tools;
using Timer = System.Timers.Timer;

namespace EasySoft.Core.Infrastructure.Configures;

/// <summary>
/// Application Configurator
/// </summary>
public static class ApplicationConfigurator
{
    /// <summary>
    /// AfterApplicationStartHandler
    /// </summary>
    public delegate void AfterApplicationStartHandler(IServiceProvider services);

    /// <summary>
    /// AfterApplicationStartHandler
    /// </summary>
    public delegate void ApplicationStoppingHandler(IServiceProvider services);

    /// <summary>
    /// OnApplicationStart
    /// </summary>
    public static event AfterApplicationStartHandler? OnApplicationStart;

    /// <summary>
    /// OnApplicationStopping
    /// </summary>
    public static event ApplicationStoppingHandler? OnApplicationStopping;

    /// <summary>
    /// Password Salt
    /// </summary>
    public static string PasswordSalt { get; set; } = "";

    private static readonly ICollection<Timer> Timers = new List<Timer>();

    private static readonly HashSet<string> Areas = new(StringComparer.OrdinalIgnoreCase);

    private static readonly List<IExtraAction<MvcOptions>> MvcOptionExtraActions = new();
    private static readonly List<IExtraAction<IEndpointRouteBuilder>> EndpointRouteBuilderExtraActions = new();
    private static readonly List<IExtraAction<WebApplicationBuilder>> WebApplicationBuilderExtraActions = new();
    private static readonly List<IExtraAction<WebApplication>> WebApplicationExtraActions = new();

    private static Action<MiniProfilerOptions>? _miniProfileOptionAction;

    static ApplicationConfigurator()
    {
        OnApplicationStart += serviceProvider =>
        {
            var environment = serviceProvider.GetService<IWebHostEnvironment>();

            if (!environment.IsDevelopment()) return;

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            loggerFactory?.CreateLogger<object>().LogAdvancePrompt(
                "Execute work after application start."
            );
        };
    }

    /// <summary>
    /// AddTimer
    /// </summary>
    public static ICollection<Timer> GetTimers()
    {
        return Timers;
    }

    /// <summary>
    /// AddTimer
    /// </summary>
    /// <param name="timers"></param>
    public static void AddTimer(params Timer[] timers)
    {
        Timers.AddRange(timers);
    }

    /// <summary>
    /// SetMiniProfilerOptionsAction
    /// </summary>
    /// <param name="action"></param>
    public static void SetMiniProfilerOptionsAction(Action<MiniProfilerOptions> action)
    {
        _miniProfileOptionAction = action;
    }

    /// <summary>
    /// GetMiniProfilerOptionsAction
    /// </summary>
    /// <returns></returns>
    public static Action<MiniProfilerOptions>? GetMiniProfilerOptionsAction()
    {
        return _miniProfileOptionAction;
    }

    /// <summary>
    /// AddArea
    /// </summary>
    /// <param name="area"></param>
    public static void AddArea(string area)
    {
        if (string.IsNullOrWhiteSpace(area)) return;

        Areas.Add(area);
    }

    /// <summary>
    /// AddAreas
    /// </summary>
    /// <param name="areas"></param>
    public static void AddAreas(params string[] areas)
    {
        Areas.Add(areas.ToListFilterNullOrWhiteSpace());
    }

    /// <summary>
    /// GetAllAreas
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> GetAllAreas()
    {
        return Areas;
    }

    /// <summary>
    /// AddMvcOptionExtraAction
    /// </summary>
    /// <param name="action"></param>
    public static void AddMvcOptionExtraAction(IExtraAction<MvcOptions> action)
    {
        MvcOptionExtraActions.Add(action);
    }

    /// <summary>
    /// GetAllMvcOptionExtraActions
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<IExtraAction<MvcOptions>> GetAllMvcOptionExtraActions()
    {
        return MvcOptionExtraActions;
    }

    /// <summary>
    /// AddEndpointRouteBuilderExtraAction
    /// </summary>
    /// <param name="action"></param>
    public static void AddEndpointRouteBuilderExtraAction(IExtraAction<IEndpointRouteBuilder> action)
    {
        EndpointRouteBuilderExtraActions.Add(action);
    }

    /// <summary>
    /// GetAllEndpointRouteBuilderExtraActions
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<IExtraAction<IEndpointRouteBuilder>> GetAllEndpointRouteBuilderExtraActions()
    {
        return EndpointRouteBuilderExtraActions;
    }

    /// <summary>
    /// AddWebApplicationBuilderExtraAction
    /// </summary>
    /// <param name="action"></param>
    public static void AddWebApplicationBuilderExtraAction(IExtraAction<WebApplicationBuilder> action)
    {
        WebApplicationBuilderExtraActions.Add(action);
    }

    /// <summary>
    /// AddWebApplicationBuilderExtraActions
    /// </summary>
    /// <param name="actions"></param>
    public static void AddWebApplicationBuilderExtraActions(params IExtraAction<WebApplicationBuilder>[] actions)
    {
        WebApplicationBuilderExtraActions.AddRange(actions);
    }

    /// <summary>
    /// GetAllWebApplicationBuilderExtraActions
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<IExtraAction<WebApplicationBuilder>> GetAllWebApplicationBuilderExtraActions()
    {
        return WebApplicationBuilderExtraActions;
    }

    /// <summary>
    /// AddWebApplicationExtraAction
    /// </summary>
    /// <param name="action"></param>
    public static void AddWebApplicationExtraAction(IExtraAction<WebApplication> action)
    {
        WebApplicationExtraActions.Add(action);
    }

    /// <summary>
    /// GetAllWebApplicationExtraActions
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<IExtraAction<WebApplication>> GetAllWebApplicationExtraActions()
    {
        return WebApplicationExtraActions;
    }

    /// <summary>
    /// DoAfterApplicationStart
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static void DoAfterApplicationStart(IServiceProvider serviceProvider)
    {
        OnApplicationStart?.Invoke(serviceProvider);
    }

    /// <summary>
    /// DoAfterApplicationStart
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static void DoWhenApplicationStopping(IServiceProvider serviceProvider)
    {
        OnApplicationStopping?.Invoke(serviceProvider);
    }
}