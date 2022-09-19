using System.Text;
using EasySoft.Core.ConsulClient.Assists;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

/// <summary>
/// ConsulController
/// </summary>
public class ConsulController : AreaControllerCore
{
    /// <summary>
    /// Index
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        var consulClient = ConsulClientAssist.GetConfigClient();

        var v = await consulClient.KV.Get("100/config.Development.json");

        var vv = Encoding.UTF8.GetString(v.Response.Value, 0, v.Response.Value.Length);

        return Content(vv);
    }

    /// <summary>
    /// TestNlog
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public IActionResult TestNlog(IConfiguration configuration)
    {
        return Ok(configuration["NLog"]);
    }
}