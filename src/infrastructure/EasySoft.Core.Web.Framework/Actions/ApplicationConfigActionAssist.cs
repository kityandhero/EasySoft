using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.Web.Framework.Assists;

public static class ApplicationConfigActionAssist
{
    private static List<string> _areas = new();
    private static readonly List<Action<IEndpointRouteBuilder>> EndpointActions = new();
    private static readonly List<Action<MvcOptions>> MvcOptionActions = new();

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

    public static void AddEndpointAction(Action<IEndpointRouteBuilder> endpointAction)
    {
        EndpointActions.Add(endpointAction);
    }

    public static List<Action<IEndpointRouteBuilder>> GetEndpointActionCollection()
    {
        return EndpointActions;
    }

    public static void AddMvcOptionAction(Action<MvcOptions> mvcOptionAction)
    {
        MvcOptionActions.Add(mvcOptionAction);
    }

    public static List<Action<MvcOptions>> GetMvcOptionActionCollection()
    {
        return MvcOptionActions;
    }
}