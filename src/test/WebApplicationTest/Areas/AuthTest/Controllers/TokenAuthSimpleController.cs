using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.AuthTest.Controllers;

/// <summary>
/// TokenAuthSimpleController
/// </summary>
[Operator]
public class TokenAuthSimpleController : AreaControllerCore
{
    /// <summary>
    /// NeedAuth
    /// </summary>
    /// <returns></returns>
    public IActionResult NeedAuth()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// NeedPermission
    /// </summary>
    /// <returns></returns>
    [GuidTag("356316bbf81e4cda93ab9a1238765875")]
    public IActionResult NeedPermission()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }
}