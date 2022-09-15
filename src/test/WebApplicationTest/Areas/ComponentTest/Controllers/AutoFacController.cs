using AutoFacTest.Interfaces;
using EasySoft.Core.AutoFac.Attributes;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

/// <summary>
/// AutoFacController
/// </summary>
public class AutoFacController : AreaControllerCore
{
    /// <summary>
    /// SimpleAutowired
    /// </summary>
    [Autowired]
    private ISimple? SimpleAutowired { get; set; }

    private readonly ISimple _simple;

    /// <summary>
    /// AutoFacController
    /// </summary>
    /// <param name="simple"></param>
    public AutoFacController(ISimple simple)
    {
        _simple = simple;
    }

    // GET  
    /// <summary>
    /// Index
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return this.Success(new
        {
            value = _simple.GetValue().ToString(),
            info = "构造函数注入"
        });
    }

    /// <summary>
    /// TestCustom
    /// </summary>
    /// <returns></returns>
    public IActionResult TestCustom()
    {
        var simple = AutofacAssist.Instance.Resolve<ISimple>();

        return this.Success(new
        {
            value = simple.GetValue().ToString(),
            info = "自主构建"
        });
    }

    /// <summary>
    /// TestAutowired
    /// </summary>
    /// <returns></returns>
    public IActionResult TestAutowired()
    {
        return this.Success(new
        {
            value = SimpleAutowired?.GetValue().ToString(),
            info = "属性注入"
        });
    }
}