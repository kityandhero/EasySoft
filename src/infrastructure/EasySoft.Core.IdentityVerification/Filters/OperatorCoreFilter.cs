using System.ComponentModel;
using EasySoft.Core.IdentityVerification.Attributes;
using EasySoft.Core.IdentityVerification.ExtensionMethods;
using EasySoft.Core.IdentityVerification.Officers;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.IdentityVerification.Filters;

public abstract class OperatorCoreFilter : OperateOfficerCore, IOperatorAuthorizationFilter
{
    public void AdjustAccessPermission(AuthorizationFilterContext filterContext)
    {
        if (filterContext.ActionDescriptor is not ControllerActionDescriptor actionDescriptor)
        {
            return;
        }

        var hasOperatorAttribute = actionDescriptor.MethodInfo.ContainAttribute<OperatorAttribute>();

        if (!hasOperatorAttribute)
        {
            return;
        }

        var guidTagAttribute = actionDescriptor.MethodInfo.TryGetAttribute<GuidTagAttribute>();

        if (guidTagAttribute == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(guidTagAttribute.GuidTag))
        {
            return;
        }

        AccessPermission.Url = filterContext.HttpContext.Request.GetUrl();
        AccessPermission.Path = filterContext.HttpContext.Request.GetAbsolutePath();

        var descriptionAttribute = filterContext.ActionDescriptor.TryGetAttribute<DescriptionAttribute>();
        var competenceConfig = filterContext.ActionDescriptor.TryGetAttribute<CompetenceConfigAttribute>();

        AccessPermission.Name = descriptionAttribute?.Description ?? AccessPermission.Path;
        AccessPermission.Competence = competenceConfig?.ToString() ?? "";
        AccessPermission.GuidTag = guidTagAttribute.GuidTag;
    }

    [Description("验证登录凭证以及操作权限")]
    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        var hasOperatorAttribute =
            filterContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor &&
            controllerActionDescriptor.MethodInfo.ContainAttribute<OperatorAttribute>();

        if (!hasOperatorAttribute)
        {
            return;
        }

        AdjustAccessPermission(filterContext);

        var token = filterContext.HttpContext.GetToken();

        var result = TryAuthorization(token);

        if (!result.Success)
        {
            filterContext.Result = result.Data;
        }
    }
}