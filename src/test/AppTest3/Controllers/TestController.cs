using EasySoft.UtilityTools.Core.Results;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace AppTest3.Controllers;

public class TestController : Controller
{
    private readonly DataContext _dataContext;

    public TestController(ILogger<TestController> logger, DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IActionResult Index()
    {
        return Content("Success");
    }

    public IActionResult Test()
    {
        _dataContext.Database.EnsureCreated();

        var data = ReturnCode.Ok.ToMessage();

        return new ApiResult(ReturnCode.Ok, data.Success, data.Message);
    }
}