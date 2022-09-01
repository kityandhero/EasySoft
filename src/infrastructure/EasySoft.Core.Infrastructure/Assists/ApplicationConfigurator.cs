using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StackExchange.Profiling;

namespace EasySoft.Core.Infrastructure.Assists;

public static class ApplicationConfigurator
{
    private static readonly HashSet<string> Areas = new(StringComparer.OrdinalIgnoreCase);
    private static readonly List<IExtraAction<MvcOptions>> MvcOptionExtraActions = new();
    private static readonly List<IExtraAction<IEndpointRouteBuilder>> EndpointRouteBuilderExtraActions = new();
    private static readonly List<IExtraAction<WebApplicationBuilder>> WebApplicationBuilderExtraActions = new();
    private static readonly List<IExtraAction<WebApplication>> WebApplicationExtraActions = new();

    private static Action<MiniProfilerOptions>? _miniProfileOptionAction;

    public static void SetMiniProfilerOptionsAction(Action<MiniProfilerOptions> action)
    {
        _miniProfileOptionAction = action;
    }

    public static Action<MiniProfilerOptions>? GetMiniProfilerOptionsAction()
    {
        return _miniProfileOptionAction;
    }

    public static void AddArea(string area)
    {
        if (string.IsNullOrWhiteSpace(area))
        {
            return;
        }

        Areas.Add(area);
    }

    public static void AddAreas(params string[] areas)
    {
        areas.ForEach(AddArea);
    }

    public static IEnumerable<string> GetAllAreas()
    {
        return Areas;
    }

    public static void AddMvcOptionExtraAction(IExtraAction<MvcOptions> action)
    {
        MvcOptionExtraActions.Add(action);
    }

    public static IEnumerable<IExtraAction<MvcOptions>> GetAllMvcOptionExtraActions()
    {
        return MvcOptionExtraActions;
    }

    public static void AddEndpointRouteBuilderExtraAction(IExtraAction<IEndpointRouteBuilder> action)
    {
        EndpointRouteBuilderExtraActions.Add(action);
    }

    public static IEnumerable<IExtraAction<IEndpointRouteBuilder>> GetAllEndpointRouteBuilderExtraActions()
    {
        return EndpointRouteBuilderExtraActions;
    }

    public static void AddWebApplicationBuilderExtraAction(IExtraAction<WebApplicationBuilder> action)
    {
        WebApplicationBuilderExtraActions.Add(action);
    }

    public static void AddWebApplicationBuilderExtraActions(params IExtraAction<WebApplicationBuilder>[] actions)
    {
        WebApplicationBuilderExtraActions.AddRange(actions);
    }

    public static IEnumerable<IExtraAction<WebApplicationBuilder>> GetAllWebApplicationBuilderExtraActions()
    {
        return WebApplicationBuilderExtraActions;
    }

    public static void AddWebApplicationExtraAction(IExtraAction<WebApplication> action)
    {
        WebApplicationExtraActions.Add(action);
    }

    public static IEnumerable<IExtraAction<WebApplication>> GetAllWebApplicationExtraActions()
    {
        return WebApplicationExtraActions;
    }
}