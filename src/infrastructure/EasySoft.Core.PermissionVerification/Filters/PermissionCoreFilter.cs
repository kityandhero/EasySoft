﻿using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Officers;

namespace EasySoft.Core.PermissionVerification.Filters;

/// <summary>
/// PermissionCoreFilter
/// </summary>
public abstract class PermissionCoreFilter : OperateOfficerCore, IPermissionFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    protected PermissionCoreFilter(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment
    ) : base(loggerFactory, environment)
    {
    }

    /// <summary>
    /// AdjustAccessPermission
    /// </summary>
    /// <param name="filterContext"></param>
    public void AdjustAccessPermission(ActionExecutingContext filterContext)
    {
        if (filterContext.ActionDescriptor is not ControllerActionDescriptor actionDescriptor) return;

        var permissionAttribute = actionDescriptor.MethodInfo.TryGetCustomAttribute<PermissionAttribute>();

        if (permissionAttribute == null) return;

        if (string.IsNullOrWhiteSpace(permissionAttribute.GuidTag)) return;

        AccessPermission.Url = filterContext.HttpContext.Request.GetUrl();
        AccessPermission.Path = filterContext.HttpContext.Request.GetAbsolutePath();

        AccessPermission.Name = string.IsNullOrWhiteSpace(permissionAttribute.Name)
            ? AccessPermission.Path
            : permissionAttribute.Name;
        AccessPermission.Competence = permissionAttribute.AggregateExpandItems();
        AccessPermission.GuidTag = permissionAttribute.GuidTag;
    }

    [Description("验证登录凭证以及操作权限")]
    private async Task OnVerificationAsync(ActionExecutingContext context)
    {
        AdjustAccessPermission(context);

        var result = await TryVerificationAsync();

        if (!result.Success) context.Result = result.Data;
    }

    /// <summary>
    /// OnActionExecuting
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Task.FromResult(OnVerificationAsync(context));
    }

    /// <summary>
    /// OnActionExecuted
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}