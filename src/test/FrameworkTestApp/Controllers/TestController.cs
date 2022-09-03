using EasySoft.Core.Web.Framework.Controllers;
using EasySoft.UtilityTools.Core.Results;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.Contexts;
using Microsoft.AspNetCore.Mvc;

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