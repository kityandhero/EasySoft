using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Officers.Interfaces;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.PermissionVerification.Officers.Implements;

/// <summary>
/// OperateOfficer
/// </summary>
public class OperateOfficer : OperateOfficerCore, IOperateOfficer
{
    /// <summary>
    /// OperateOfficer
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    /// <param name="mediator"></param>
    public OperateOfficer(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IMediator mediator
    ) : base(loggerFactory, environment, mediator)
    {
    }

    /// <summary>
    /// AdjustAccessPermission
    /// </summary>
    /// <param name="httpContext"></param>
    public void AdjustAccessPermission(HttpContext httpContext)
    {
        var permissionAttribute = httpContext.TryGetMetadata<PermissionAttribute>();

        if (permissionAttribute == null) return;

        if (string.IsNullOrWhiteSpace(permissionAttribute.GuidTag)) return;

        AccessPermission.Url = httpContext.Request.GetUrl();
        AccessPermission.Path = httpContext.Request.GetAbsolutePath();

        AccessPermission.Name = string.IsNullOrWhiteSpace(permissionAttribute.Name)
            ? AccessPermission.Path
            : permissionAttribute.Name;
        AccessPermission.Competence = permissionAttribute.AggregateExpandItems();
        AccessPermission.GuidTag = permissionAttribute.GuidTag;
    }

    /// <summary>
    /// DoVerification
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    [Description("验证登录凭证以及操作权限")]
    public async Task<ExecutiveResult<ApiResult>> DoVerificationAsync(HttpContext httpContext)
    {
        AdjustAccessPermission(httpContext);

        return await TryVerificationAsync();
    }
}