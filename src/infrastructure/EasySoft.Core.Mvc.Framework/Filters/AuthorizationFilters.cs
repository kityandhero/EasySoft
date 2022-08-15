using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.Mvc.Framework.Filters;

/// <summary>
/// 身份认证类继承IAuthorizationFilter接口
/// </summary>
public abstract class BaseAuthorizationFilters : IAuthorizationFilter
{
    /// <summary>
    ///  请求验证，当前验证部分不要抛出异常，ExceptionFilter不会处理
    /// </summary>
    /// <param name="context">请求内容信息</param>
    public abstract void OnAuthorization(AuthorizationFilterContext context);
}