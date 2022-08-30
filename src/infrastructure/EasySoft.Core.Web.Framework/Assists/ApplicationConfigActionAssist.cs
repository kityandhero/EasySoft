using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.Web.Framework.Assists;

public static class ApplicationConfigActionAssist
{
    private static List<string> _areas = new();
    private static readonly List<Action<MvcOptions>> MvcOptionActions = new();
    private static readonly List<Action<IEndpointRouteBuilder>> EndpointRouteBuilderActions = new();

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
}