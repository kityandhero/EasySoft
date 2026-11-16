using EasySoft.Core.Web.Framework.Kernel.Attributes;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace EasySoft.Core.Web.Framework.Kernel.ActionModelConventions;

/// <summary>
/// 标记某些 Controller/Action，隐藏在 Swagger/ApiExplorer。
/// </summary>
public class IgnoreForApiExplorerActionModelConvention : IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        var marked = action.Attributes.Any(x => x is IgnoreActionAttribute)
                     || action.Controller.Attributes.Any(x => x is IgnoreActionAttribute);

        if (marked)
        {
            action.ApiExplorer.IsVisible = false;
        }
    }
}