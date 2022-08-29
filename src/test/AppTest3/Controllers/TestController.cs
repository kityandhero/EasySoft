using EasySoft.UtilityTools.Core.Results;
using EntityFrameworkTest.Contexts;
using Microsoft.AspNetCore.Mvc;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace AppTest3.Controllers;

public class TestController : Controller
{
    private ILogger<TestController> _logger;
    private readonly DataContext _dataContext;

    public TestController(ILogger<TestController> logger, DataContext dataContext)
    {
        _logger = logger;
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