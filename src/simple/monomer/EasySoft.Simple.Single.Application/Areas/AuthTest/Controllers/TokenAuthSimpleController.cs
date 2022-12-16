using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Simple.Single.Application.Areas.AuthTest.Controllers;

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
    [Permission("356316bbf81e4cda93ab9a1238765875")]
    public IActionResult NeedPermission()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }
}