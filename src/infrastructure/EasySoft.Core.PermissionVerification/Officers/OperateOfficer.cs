using EasySoft.Core.PermissionVerification.Attributes;

namespace EasySoft.Core.PermissionVerification.Officers;

public class OperateOfficer : OperateOfficerCore, IOperateOfficer
{
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

    [Description("验证登录凭证以及操作权限")]
    public ExecutiveResult<ApiResult> DoVerification(HttpContext httpContext)
    {
        AdjustAccessPermission(httpContext);

        return TryVerification();
    }
}