using EasySoft.Core.Swagger.Configures;

namespace EasySoft.Core.Swagger.ModelConventions;

/// <summary>
/// IgnoreActionModelConvention.
/// <remarks>Assembly names that satisfy the ignore condition ignore the generated document.</remarks>
/// </summary>
public class IgnoreActionModelConvention : IActionModelConvention
{
    /// <summary>
    /// Apply
    /// </summary>
    /// <param name="action"></param>
    public void Apply(ActionModel action)
    {
        var ignoreRoutesAssemblyFilters = SwaggerConfigure.IgnoreRoutesAssemblyFilters;

        if (!ignoreRoutesAssemblyFilters.Keys.Contains("Ocelot"))
            ignoreRoutesAssemblyFilters.Add("Ocelot", (source, pattern) => source.StartsWith(pattern));

        var assemblyFullName = action.ActionMethod.Module.Assembly.GetName().Name;

        if (string.IsNullOrWhiteSpace(assemblyFullName)) return;

        if (ignoreRoutesAssemblyFilters.Any(o => o.Value.Invoke(assemblyFullName, o.Key)))
            action.ApiExplorer.IsVisible = false;
    }
}