using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

/// <summary>
/// Ocelot
/// </summary>
public class OcelotController : AreaControllerCore
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