using EasySoft.Core.DependencyInjection.IocAssists;
using EasySoft.Simple.Single.Application.Common;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

/// <summary>
/// AutoFacController
/// </summary>
public class AutoFacController : AreaControllerCore
{
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
        return this.Success(
            new
            {
                value = _simple.GetValue().ToString(),
                info = "构造函数注入"
            }
        );
    }

    /// <summary>
    /// TestCustom
    /// </summary>
    /// <returns></returns>
    public IActionResult TestCustom()
    {
        var simple = ServiceProviderAssist.Instance.Resolve<ISimpleDependencyInjection>();

        return this.Success(
            new
            {
                value = simple.GetValue().ToString(),
                info = "自主构建"
            }
        );
    }
}