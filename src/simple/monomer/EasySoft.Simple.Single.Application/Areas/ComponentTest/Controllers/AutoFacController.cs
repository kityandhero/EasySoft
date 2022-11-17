using EasySoft.Core.AutoFac.Attributes;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Simple.Single.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

/// <summary>
/// AutoFacController
/// </summary>
public class AutoFacController : AreaControllerCore
{
    /// <summary>
    /// SimpleAutowired
    /// </summary>
    [Autowired]
    private ISimpleDependencyInjection? SimpleAutowired { get; set; }

    private readonly ISimpleDependencyInjection _simple;

    /// <summary>
    /// AutoFacController
    /// </summary>
    /// <param name="simple"></param>
    public AutoFacController(ISimpleDependencyInjection simple)
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
        var simple = AutofacAssist.Instance.Resolve<ISimpleDependencyInjection>();

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