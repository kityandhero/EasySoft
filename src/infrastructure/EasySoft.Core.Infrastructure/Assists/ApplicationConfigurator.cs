using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using StackExchange.Profiling;

namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// Application Configurator
/// </summary>
public static class ApplicationConfigurator
{
    /// <summary>
    /// Password Salt
    /// </summary>
    public static string PasswordSalt { get; set; } = "";

    private static readonly HashSet<string> Areas = new(StringComparer.OrdinalIgnoreCase);
    private static readonly List<IExtraAction<MvcOptions>> MvcOptionExtraActions = new();
    private static readonly List<IExtraAction<IEndpointRouteBuilder>> EndpointRouteBuilderExtraActions = new();
    private static readonly List<IExtraAction<WebApplicationBuilder>> WebApplicationBuilderExtraActions = new();
    private static readonly List<IExtraAction<WebApplication>> WebApplicationExtraActions = new();

    private static Action<MiniProfilerOptions>? _miniProfileOptionAction;

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
}