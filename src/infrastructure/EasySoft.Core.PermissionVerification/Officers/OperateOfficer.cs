using EasySoft.Core.PermissionVerification.Attributes;

namespace EasySoft.Core.PermissionVerification.Officers;

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
    public OperateOfficer(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment
    ) : base(loggerFactory, environment)
    {
    }

    /// <summary>
    /// AdjustAccessPermission
    /// </summary>
    /// <param name="httpContext"></param>
    public void AdjustAccessPermission(HttpContext httpContext)
    {
        var guidTagAttribute = httpContext.TryGetMetadata<GuidTagAttribute>();

        if (guidTagAttribute == null) return;

        if (string.IsNullOrWhiteSpace(guidTagAttribute.GuidTag)) return;

        AccessPermission.Url = httpContext.Request.GetUrl();
        AccessPermission.Path = httpContext.Request.GetAbsolutePath();

        var descriptionAttribute = httpContext.TryGetMetadata<DescriptionAttribute>();
        var competenceConfig = httpContext.TryGetMetadata<CompetenceConfigAttribute>();

        AccessPermission.Name = descriptionAttribute?.Description ?? AccessPermission.Path;
        AccessPermission.Competence = competenceConfig?.ToString() ?? "";
        AccessPermission.GuidTag = guidTagAttribute.GuidTag;
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