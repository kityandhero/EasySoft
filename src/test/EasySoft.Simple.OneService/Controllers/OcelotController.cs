using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.OneService.Controllers;

/// <summary>
/// Ocelot
/// </summary>
public class OcelotController : CustomControllerBase
{
    /// <summary>
    /// 测认识请求超时
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult TestTimeOut()
    {
        Thread.Sleep(10000);

        return this.Success();
    }
}