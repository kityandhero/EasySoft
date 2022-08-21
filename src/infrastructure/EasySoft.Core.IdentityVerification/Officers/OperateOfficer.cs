using System.ComponentModel;
using EasySoft.Core.IdentityVerification.Attributes;
using EasySoft.Core.IdentityVerification.ExtensionMethods;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.ExtensionMethods;
using EasySoft.UtilityTools.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.IdentityVerification.Officers;

public class OperateOfficer : OperateOfficerCore, IOperateOfficer
{
    public void AdjustAccessPermission(HttpContext httpContext)
    {
        var operatorAttribute = httpContext.TryGetAttribute<OperatorAttribute>();

        if (operatorAttribute == null)
        {
            return;
        }

        var guidTagAttribute = httpContext.TryGetAttribute<GuidTagAttribute>();

        if (guidTagAttribute == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(guidTagAttribute.GuidTag))
        {
            return;
        }

        AccessPermission.Url = httpContext.Request.GetUrl();
        AccessPermission.Path = httpContext.Request.GetAbsolutePath();

        var descriptionAttribute = httpContext.TryGetAttribute<DescriptionAttribute>();
        var competenceConfig = httpContext.TryGetAttribute<CompetenceConfigAttribute>();

        AccessPermission.Name = descriptionAttribute?.Description ?? AccessPermission.Path;
        AccessPermission.Competence = competenceConfig?.ToString() ?? "";
        AccessPermission.GuidTag = guidTagAttribute.GuidTag;
    }

    [Description("验证登录凭证以及操作权限")]
    public ExecutiveResult<JsonResult> DoAuthorization(HttpContext httpContext)
    {
        var hasOperatorAttribute = httpContext.TryGetAttribute<OperatorAttribute>();

        if (hasOperatorAttribute == null)
        {
            return new ExecutiveResult<JsonResult>(ReturnCode.Ok);
        }

        AdjustAccessPermission(httpContext);

        var token = httpContext.GetToken();

        return TryAuthorization(token);
    }
}