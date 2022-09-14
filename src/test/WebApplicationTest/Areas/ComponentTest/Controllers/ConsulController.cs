using System.Buffers.Text;
using System.Text;
using Consul;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Media.Image;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class ConsulController : AreaControllerCore
{
    public async Task<IActionResult> Index(IConsulClient consulClient1)
    {
        var consulClient = new ConsulClient(x => { x.Address = new Uri(ConsulConfigAssist.GetConsulAddress()); });

        // var cc = AutofacAssist.Instance.Resolve<IConsulClient>();

        var v = await consulClient.KV.Get("100/config.Development.json");

        var vv = Encoding.UTF8.GetString(v.Response.Value, 0, v.Response.Value.Length);

        var nlog = ConsulConfigAssist.GetValue("NLog");

        return Content(vv);
    }

    public IActionResult TestNlog(IConfiguration configuration)
    {
        return Ok(configuration["NLog"]);
    }
}