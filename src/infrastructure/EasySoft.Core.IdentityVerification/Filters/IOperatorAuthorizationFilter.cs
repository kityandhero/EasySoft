using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.IdentityVerification.Filters;

public interface IOperatorAuthorizationFilter : IAuthorizationFilter
{
    void AdjustAccessPermission(AuthorizationFilterContext filterContext);
}