using EasySoft.Core.EasyToken.Attributes;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.Web.Framework.Controllers;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

[Operator]
public class EasyTokenAuthSimpleController : CustomControllerBase
{
    public IActionResult NeedAuth()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }

    [GuidTag("356316bbf81e4cda93ab9a1238765875")]
    public IActionResult NeedPermission()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }
}