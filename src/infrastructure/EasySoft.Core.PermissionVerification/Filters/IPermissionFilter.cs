using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.PermissionVerification.Filters;

public interface IPermissionFilter : IActionFilter
{
    void AdjustAccessPermission(ActionExecutingContext filterContext);
}