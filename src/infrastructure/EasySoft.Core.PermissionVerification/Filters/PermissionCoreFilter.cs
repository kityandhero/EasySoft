using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Officers;
using EasySoft.Core.PermissionVerification.Officers.Implements;
using EasySoft.UtilityTools.Core.Extensions;

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
    /// <param name="mediator"></param>
    protected PermissionCoreFilter(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IMediator mediator
    ) : base(loggerFactory, environment, mediator)
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

        var result = await TryVerifyAsync();

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