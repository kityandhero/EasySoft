using System.ComponentModel;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Officers;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.PermissionVerification.Filters;

public abstract class PermissionCoreFilter : OperateOfficerCore, IPermissionFilter
{
    public void AdjustAccessPermission(ActionExecutingContext filterContext)
    {
        if (filterContext.ActionDescriptor is not ControllerActionDescriptor actionDescriptor)
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
    private void OnVerification(ActionExecutingContext context)
    {
        AdjustAccessPermission(context);

        var result = TryVerification();

        if (!result.Success)
        {
            context.Result = result.Data;
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        OnVerification(context);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}