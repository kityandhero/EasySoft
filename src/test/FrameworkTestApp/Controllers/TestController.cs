using EntityFrameworkTest.Contexts;
using EasySoft.Core.Mvc.Framework.Controllers;
using EasySoft.Core.Mvc.Framework.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.ExtensionMethods;

namespace FrameworkTestApp.Controllers;

public class TestController : CustomControllerBase
{
    private ILogger<TestController> _logger;
    private readonly DataContext _dataContext;

    public TestController(
        ILogger<TestController> logger,
        DataContext dataContext
    )
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