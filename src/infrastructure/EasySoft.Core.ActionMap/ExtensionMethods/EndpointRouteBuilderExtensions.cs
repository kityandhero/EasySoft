using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.ActionMap.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    public static IEndpointConventionBuilder MapActionMap(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "ActionMap",
            "{controller=ActionMap}/{action=Index}"
        ).WithDisplayName("ActionMap");
    }
}