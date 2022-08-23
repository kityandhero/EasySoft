using Autofac;
using AutoFacTest.Interfaces;
using EasySoft.Core.AutoFac.Attributes;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class AutoFacController : AreaControllerCore
{
    [Autowired]
    public ISimple? SimpleAutowired { get; set; }

    private readonly ISimple _simple;

    public AutoFacController(ISimple simple)
    {
        _simple = simple;
    }

    // GET  
    public IActionResult Index()
    {
        return this.Success(new
        {
            value = _simple.GetValue().ToString(),
            info = "构造函数注入"
        });
    }

    public IActionResult TestCustom()
    {
        var simple = AutofacAssist.Instance.Container.Resolve<ISimple>();

        return this.Success(new
        {
            value = simple.GetValue().ToString(),
            info = "自主构建"
        });
    }

    public IActionResult TestAutowired()
    {
        return this.Success(new
        {
            value = SimpleAutowired?.GetValue().ToString(),
            info = "属性注入"
        });
    }
}