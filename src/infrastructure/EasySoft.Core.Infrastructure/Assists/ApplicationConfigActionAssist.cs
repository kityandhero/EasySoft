using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.Infrastructure.Assists;

public static class ApplicationConfigActionAssist
{
    private static List<string> _areas = new();
    private static readonly List<Action<MvcOptions>> MvcOptionActions = new();
    private static readonly List<Action<IEndpointRouteBuilder>> EndpointRouteBuilderActions = new();
    private static readonly List<Action<WebApplicationBuilder>> WebApplicationBuilderActions = new();
    private static readonly List<Action<WebApplication>> WebApplicationActions = new();

    public static void AddArea(string area)
    {
        _areas.Add(area);

        _areas = _areas.Distinct().ToListFilterNullOrWhiteSpace();
    }

    public static void AddAreas(params string[] areas)
    {
        _areas.AddRange(areas);

        _areas = _areas.Distinct().ToListFilterNullOrWhiteSpace();
    }

    public static IEnumerable<string> GetAreaCollection()
    {
        return _areas;
    }

    public static void AddMvcOptionAction(Action<MvcOptions> action)
    {
        MvcOptionActions.Add(action);
    }

    public static IEnumerable<Action<MvcOptions>> GetMvcOptionActionCollection()
    {
        return MvcOptionActions;
    }

    public static void AddEndpointRouteBuilderAction(Action<IEndpointRouteBuilder> action)
    {
        EndpointRouteBuilderActions.Add(action);
    }

    public static IEnumerable<Action<IEndpointRouteBuilder>> GetEndpointRouteBuilderActionCollection()
    {
        return EndpointRouteBuilderActions;
    }

    public static void AddWebApplicationBuilderAction(Action<WebApplicationBuilder> action)
    {
        WebApplicationBuilderActions.Add(action);
    }

    public static void AddWebApplicationBuilderActions(params Action<WebApplicationBuilder>[] actions)
    {
        WebApplicationBuilderActions.AddRange(actions);
    }

    public static IEnumerable<Action<WebApplicationBuilder>> GetWebApplicationBuilderActionCollection()
    {
        return WebApplicationBuilderActions;
    }

    public static void AddWebApplicationAction(Action<WebApplication> action)
    {
        WebApplicationActions.Add(action);
    }

    public static IEnumerable<Action<WebApplication>> GetWebApplicationActionCollection()
    {
        return WebApplicationActions;
    }
}