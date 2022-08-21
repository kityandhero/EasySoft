using Autofac;
using AutoFacTest.Interfaces;
using EasyCaching.Core;
using EasySoft.Core.AutoFac.Attributes;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Web.Framework.Attributes;
using EasySoft.Core.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class JobController : CustomControllerBase
{
    [Autowired]
    public ISimple? SimpleAutowired { get; set; }

    // GET  
    public IActionResult Index()
    {
        return Content("Success");
    }

    public IActionResult TestAutoFac()
    {
        var simple = AutofacAssist.Instance.Container.Resolve<ISimple>();

        return Content(simple.GetValue().ToString());
    }

    public IActionResult TestAutowired()
    {
        return Content(SimpleAutowired!.GetValue().ToString());
    }
}